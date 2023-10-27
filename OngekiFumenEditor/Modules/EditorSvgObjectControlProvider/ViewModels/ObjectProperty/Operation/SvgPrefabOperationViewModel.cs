﻿using Caliburn.Micro;
using OngekiFumenEditor.Base.EditorObjects.Svg;
using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Base.OngekiObjects.ConnectableObject;
using OngekiFumenEditor.Base.OngekiObjects.Lane;
using OngekiFumenEditor.Base.OngekiObjects.Lane.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor;
using OngekiFumenEditor.Modules.FumenVisualEditor.Base;
using OngekiFumenEditor.Modules.FumenVisualEditor.Kernel;
using OngekiFumenEditor.Modules.FumenVisualEditor.ViewModels;
using OngekiFumenEditor.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Media;

namespace OngekiFumenEditor.Modules.EditorSvgObjectControlProvider.ViewModels.ObjectProperty.Operation
{
	public class SvgPrefabOperationViewModel : PropertyChangedBase
	{
		public SvgPrefabBase SvgPrefab { get; }

		public SvgPrefabOperationViewModel(SvgPrefabBase svgPrefab)
		{
			SvgPrefab = svgPrefab;
		}

		public void OnGenerateLaneToEditor()
		{
			if (IoC.Get<IEditorDocumentManager>().CurrentActivatedEditor is not FumenVisualEditorViewModel editor)
			{
				MessageBox.Show("请先切换到当前编辑器");
				return;
			}

			if (!editor.IsDesignMode)
			{
				MessageBox.Show("请先将编辑器切换到设计模式");
				return;
			}

			if (SvgPrefab.ProcessingDrawingGroup is not DrawingGroup drawingGroup)
			{
				MessageBox.Show("无效的SVG内容");
				return;
			}

			if (SvgPrefab.ShowOriginColor)
			{
				MessageBox.Show("请先取消勾选ShowOriginColor进行将要输出轨道的预览");
				return;
			}

			var baseCanvasX = XGridCalculator.ConvertXGridToX(SvgPrefab.XGrid, editor);
			var baseCanvasY = TGridCalculator.ConvertTGridToY_DesignMode(SvgPrefab.TGrid, editor);

			var segments = SvgPrefab.GenerateLineSegments();

			var genStarts = new List<ConnectableStartObject>();

			foreach (var seg in segments)
			{
				var laneColor = SvgPrefab.PickSimilarLaneColor(seg.Color);
				var points = seg.RelativePoints;

				LaneStartBase targetObject = laneColor?.LaneType switch
				{
					LaneType.Left => new LaneLeftStart(),
					LaneType.Center => new LaneCenterStart(),
					LaneType.Right => new LaneRightStart(),
					LaneType.Colorful => new ColorfulLaneStart(),
					_ => null
				};

				if (targetObject is null)
					continue;

				void CommomBuildUp(Vector2 relativePoint, ConnectableObjectBase obj)
				{
					var actualCanvasX = baseCanvasX + relativePoint.X;
					var actualCanvasY = baseCanvasY + relativePoint.Y;

					//Log.LogDebug($"{relativePoint}  ->  {new Vector2((float)actualCanvasX, (float)actualCanvasY)}");
					var tGrid = TGridCalculator.ConvertYToTGrid_DesignMode(actualCanvasY, editor);
					var xGrid = XGridCalculator.ConvertXToXGrid(actualCanvasX, editor);

					obj.XGrid = xGrid;
					obj.TGrid = tGrid;
				}

				var firstP = points[0];
				var startObj = LambdaActivator.CreateInstance(targetObject.GetType()) as ConnectableStartObject;
				CommomBuildUp(firstP, startObj);

				foreach (var childP in points.Skip(1).SkipLast(1))
				{
					var nextObj = targetObject.CreateNextObject();
					CommomBuildUp(childP, nextObj);
					startObj.AddChildObject(nextObj);
				}

				var lastP = points.LastOrDefault();
				var endObj = targetObject.CreateEndObject();
				CommomBuildUp(lastP, endObj);
				startObj.AddChildObject(endObj);

				var r = startObj.InterpolateCurve().ToArray();

				var subGenStarts = startObj.InterpolateCurve(SvgPrefab.CurveInterpolaterFactory).ToArray();
				if (targetObject is IColorfulLane lane)
				{
					//染色
					var colorId = ColorIdConst.AllColors.FirstOrDefault(x => x.Color == laneColor?.Color);
					var brightness = (int)SvgPrefab.ColorfulLaneBrightness.CurrentValue;
					subGenStarts
						.SelectMany(x => x.Children.AsEnumerable<ConnectableObjectBase>().Append(x))
						.OfType<IColorfulLane>()
						.ForEach(x =>
						{
							x.ColorId = colorId;
							x.Brightness = brightness;
						});
				}

				genStarts.AddRange(subGenStarts);
			}

			editor.UndoRedoManager.ExecuteAction(LambdaUndoAction.Create("Svg原地生成轨道物件", () =>
			{
				editor.Fumen.AddObjects(genStarts);
			}, () =>
			{
				editor.Fumen.RemoveObjects(genStarts);
			}));
		}
	}
}
