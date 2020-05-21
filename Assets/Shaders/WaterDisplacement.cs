using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

[Serializable]
[PostProcess(typeof(WaterDisplacementRenderer), PostProcessEvent.AfterStack, "Hidden/WaterDIsplacement")]
public sealed class WaterDisplacement : PostProcessEffectSettings {
    public FloatParameter Y = new FloatParameter { value = 0 };
}

public sealed class WaterDisplacementRenderer : PostProcessEffectRenderer<WaterDisplacement>{
    public override void Render(PostProcessRenderContext context) {
        PropertySheet sheet = context.propertySheets.Get(Shader.Find("Hidden/WaterDIsplacement"));
        sheet.properties.SetFloat("_MaxY", settings.Y);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);

    }
}