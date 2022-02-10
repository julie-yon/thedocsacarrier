namespace Docsa.Character
{
    public class ViewerCharacter : Character
    {
        public string ViewerName;
        public bool isViewerAssigned;
        protected override void Reset()
        {
            base.Reset();
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            ViewerName = string.Empty;
            isViewerAssigned = false;
            Reset();
        }
    }
}