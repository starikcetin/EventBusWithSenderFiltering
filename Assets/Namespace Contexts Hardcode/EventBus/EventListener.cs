namespace Albert.NsHardCode.EventBus
{
    public delegate void EventListener<in TEvent>(object sender, TEvent e);
}
