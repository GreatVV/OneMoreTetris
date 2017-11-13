using UnityEngine;


public class FigureView : MonoBehaviour, IUpdateable
{
    public Figure FigureDesc;

    public BlockView[] BlockViews;

    public void Tick()
    {
        for (var index = 0; index < FigureDesc.Blocks.Length; index++)
        {
            var figureDescBlock = FigureDesc.Blocks[index];
            if (figureDescBlock.IsDestroyed)
            {
                BlockViews[index].gameObject.SetActive(false);
            }
        }
    }
}