/**
Copyright 2011, Cong Nguyen

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
**/

using System;
using System.Collections.Generic;

using System.Text;
using IPoVn.UI;

namespace Tesseract.OCR.AppEntry.UI
{
    public abstract class Render : IRender
    {
        protected ImageViewer _owner = null;
        protected TemplateViewer _owner1 = null;

        #region IRender Members

        public virtual void DoRender(
            System.Drawing.Graphics grph, IRenderingData renderingData)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
