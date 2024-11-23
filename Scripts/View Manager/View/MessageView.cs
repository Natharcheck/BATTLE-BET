using UnityEngine;

public class MessageView : View
{
    [SerializeField] private MessagePanel prefab;
    [Space]
    [SerializeField] private int count;
    [SerializeField] private bool isExpand;
    
    private PoolMono<MessagePanel> _pool;

    private void Start()
    {
        _pool = new PoolMono<MessagePanel>(prefab, count, isExpand, transform);
    }

    public override void Initialize()
    {
        
    }
    
    public void CreateMessage(string text, MessagePanel.Type type, int lifeTime)
    {
        var messagePanel = _pool.GetFreeElement();
            messagePanel.table.text = text + " " + type;
            messagePanel.messageType = type;
            messagePanel.lifeTime = lifeTime;
            messagePanel.Initialize();
    }
}