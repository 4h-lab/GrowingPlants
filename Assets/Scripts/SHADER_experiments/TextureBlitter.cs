using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureBlitter : MonoBehaviour
{
    private Material m;
    public List<Texture2D> splats;
    private RenderTexture lightmap;

    private Material blitMat;
    // Start is called before the first frame update
    void Start()
    {
        blitMat = new Material(Shader.Find("Hidden/BlitCopy"));
        m = gameObject.GetComponent<Renderer>().material;
        foreach(Texture2D t in splats)
        {
            //t.Resize(10, 10);
        }
        lightmap = new RenderTexture(32, 32, 0, RenderTextureFormat.ARGBFloat);
       
        m.SetTexture("_ColorMaskTexture", lightmap);
        StartCoroutine(spray());
    }

    IEnumerator spray()
    {
        while (true) {
            int i = UnityEngine.Random.Range(0, splats.Count);
            var N1 = splats[i];
            var N2 = lightmap;
            blitMat.mainTexture = N1;
            blitMat.mainTextureScale = new Vector2(
                (0.1f*(float)N1.width / (float)N2.width),
                (0.1f*(float)N1.height / (float)N2.height)
            );
            blitMat.mainTextureOffset = new Vector2(
                (N2.width - N1.width) / 2f,
                (N2.height - N1.height) / 2f
            );
            Graphics.Blit(N1, N2,blitMat);

            RenderTexture temp = RenderTexture.GetTemporary(lightmap.width, lightmap.height, 0, RenderTextureFormat.ARGBFloat);
            Graphics.Blit(N2, temp);
            Graphics.Blit(temp, N2, blitMat);
            RenderTexture.ReleaseTemporary(temp);

            yield return new WaitForSeconds(1f);
        }
    }
}

