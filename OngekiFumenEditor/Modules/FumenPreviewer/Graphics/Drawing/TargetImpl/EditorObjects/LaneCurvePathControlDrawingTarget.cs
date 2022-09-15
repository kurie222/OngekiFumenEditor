﻿using Caliburn.Micro;
using OngekiFumenEditor.Base;
using OngekiFumenEditor.Base.EditorObjects.LaneCurve;
using OngekiFumenEditor.Base.OngekiObjects;
using OngekiFumenEditor.Modules.FumenPreviewer.Graphics.Drawing.DefaultDrawingImpl.StringDrawing.String;
using OngekiFumenEditor.Modules.FumenVisualEditor;
using OngekiFumenEditor.Utils;
using OngekiFumenEditor.Utils.ObjectPool;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OngekiFumenEditor.Modules.FumenPreviewer.Graphics.Drawing.TargetImpl.EditorObjects
{
    [Export(typeof(IDrawingTarget))]
    public class LaneCurvePathControlDrawingTarget : CommonBatchDrawTargetBase<LaneCurvePathControlObject>, IDisposable
    {
        private Texture texture;
        private ITextureDrawing textureDrawing;
        private IStringDrawing stringDrawing;

        public override IEnumerable<string> DrawTargetID { get; } = new[] { LaneCurvePathControlObject.CommandName };

        public LaneCurvePathControlDrawingTarget()
        {
            texture = ResourceUtils.OpenReadTextureFromResource(@"Modules\FumenVisualEditor\Views\OngekiObjects\CC.png");
            textureDrawing = IoC.Get<IBatchTextureDrawing>();
            stringDrawing = IoC.Get<IStringDrawing>();
        }

        public void Dispose()
        {
            texture = null;
            texture.Dispose();
        }

        public override void DrawBatch(IFumenPreviewer target, IEnumerable<LaneCurvePathControlObject> objs)
        {
            var size = new Vector2(16, 16);

            using var d = objs.Select(x => (
                (float)TGridCalculator.ConvertTGridToY(x.TGrid, target.Fumen.BpmList, 1, 240),
                (float)XGridCalculator.ConvertXGridToX(x.XGrid, 30, target.ViewWidth, 1),
                x
            )).ToHashSetWithObjectPool<(float y, float x, LaneCurvePathControlObject obj)>(out var list);

            textureDrawing.Draw(target, texture, list.Select(x => (size, new Vector2(x.x, x.y), 0f)));

            foreach (var item in list)
                stringDrawing.Draw(item.obj.Index.ToString(), new(item.x, item.y + 4), Vector2.One, 15, 0, new(1, 0, 1, 1), new(0.5f, 0.5f),
                     IStringDrawing.StringStyle.Bold, target, default, out _);
        }
    }
}
