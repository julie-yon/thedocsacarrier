namespace Docsa
{
    public class RandomItem : Item
    {
        protected override void Awake()
        {
            base.Awake();
            ItemEvent.AddListener(Effect);
        }

        public override void Effect()
        {
            print("Random Item Effect");

            base.Effect();
        }
    }
}