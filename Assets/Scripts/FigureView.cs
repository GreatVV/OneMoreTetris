using UnityEngine;


public class FigureView : MonoBehaviour, IExecuteSystem
{
    public Figure FigureDesc;

    public BlockView[] BlockViews;
    [SerializeField] public RectTransform Transform;    

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