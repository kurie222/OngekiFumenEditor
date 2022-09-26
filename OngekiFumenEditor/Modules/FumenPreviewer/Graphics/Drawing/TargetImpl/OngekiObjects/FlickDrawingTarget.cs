﻿using Caliburn.Micro;
using OngekiFumenEditor.Base;
using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Modules.FumenVisualEditor;
using OngekiFumenEditor.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OngekiFumenEditor.Modules.FumenPreviewer.Graphics.Drawing.TargetImpl.OngekiObjects
{
    [Export(typeof(IDrawingTarget))]
    public class FlickDrawingTarget : CommonBatchDrawTargetBase<Flick>, IDisposable
    {
        private Texture texture;
        private Texture exFlickEffTexture;

        private Vector2 leftSize;
        private Vector2 rightSize;
        private Vector2 exTapEffSize;
        private Vector2 selectedEffSize;

        private List<(Vector2, Vector2, float)> exFlickList = new();
        private List<(Vector2, Vector2, float)> selectedFlickList = new();
        private List<(Vector2, Vector2, float)> normalFlichList = new();

        private IBatchTextureDrawing batchTextureDrawing;
        private IHighlightBatchTextureDrawing highlightDrawing;

        public override IEnumerable<string> DrawTargetID { get; } = new[] { "FLK", "CFK" };

        public FlickDrawingTarget() : base()
        {
            texture = ResourceUtils.OpenReadTextureFromResource(@"Modules\FumenVisualEditor\Views\OngekiObjects\flick.png");
            exFlickEffTexture = ResourceUtils.OpenReadTextureFromResource(@"Modules\FumenVisualEditor\Views\OngekiObjects\exflick_Eff.png");

            leftSize = new Vector2(104, 69.333f);
            rightSize = new Vector2(-104, 69.333f);
            exTapEffSize = new Vector2(106, 67f);
            selectedEffSize = new Vector2(106, 67f) * 1.05f;

            batchTextureDrawing = IoC.Get<IBatchTextureDrawing>();
            highlightDrawing = IoC.Get<IHighlightBatchTextureDrawing>();
        }

        public override void DrawBatch(IFumenPreviewer target, IEnumerable<Flick> objs)
        {
            foreach (var obj in objs)
            {
                var x = XGridCalculator.ConvertXGridToX(obj.XGrid, 30, target.ViewWidth, 1);
                var y = TGridCalculator.ConvertTGridToY(obj.TGrid, target.Fumen.BpmList, 1.0, 240) + 24;
                var pos = new Vector2((float)x, (float)y);
                var size = obj.Direction == Flick.FlickDirection.Right ? rightSize : leftSize;
                normalFlichList.Add((size, pos, 0f));

                if (obj.IsCritical)
                {
                    var exTapSize = exTapEffSize;
                    exTapSize.X = Math.Sign(size.X) * exTapSize.X;
                    pos.Y -= 1;

                    exFlickList.Add((exTapSize, pos, 0));
                }

                if (obj.IsSelected)
                {
                    var selectTapSize = selectedEffSize;
                    selectTapSize.X = Math.Sign(size.X) * selectTapSize.X;
                    pos.Y -= 1;

                    selectedFlickList.Add((selectTapSize, pos, 0));
                }

                size.X = Math.Abs(size.X);
                target.RegisterSelectableObject(obj, pos, size);
            }

            highlightDrawing.Draw(target, texture, selectedFlickList);
            batchTextureDrawing.Draw(target, texture, normalFlichList);
            batchTextureDrawing.Draw(target, exFlickEffTexture, exFlickList);

            exFlickList.Clear();
            selectedFlickList.Clear();
            normalFlichList.Clear();
        }

        public void Dispose()
        {
            texture?.Dispose();
            texture = null;
        }
    }
}
