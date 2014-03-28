using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace Caltron.Controls
{
    public class ModelControl : Control
    {
        private ModelObjectModel mvarModel = null;
        /// <summary>
        /// The model to display on this control.
        /// </summary>
        public ModelObjectModel Model { get { return mvarModel; } set { mvarModel = value; } }

        protected internal override void OnRender(RenderEventArgs e)
        {
            base.OnRender(e);

            e.Canvas.DrawModel(mvarModel);
        }
    }
}
