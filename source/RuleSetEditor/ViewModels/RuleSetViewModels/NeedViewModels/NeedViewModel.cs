using RuleSet;

namespace RuleSetEditor.ViewModels.RuleSetViewModels.NeedViewModels
{
    public class NeedViewModel : RuleSetViewModelBase
    {
        private Need need;

        public Need Need
        {
            get
            {
                return need;
            }
            set
            {
                if (!RaiseSetIfChanged(ref need, value))
                    return;
                if (!DeferChanged)
                    OnElementChanged();
                else
                    DeferQueue.Enqueue(OnElementChanged);
            }
        }

        protected virtual void OnElementChanged()
        {
        }
    }

    public class NeedViewModel<T> : NeedViewModel where T : Need
    {
        public new T Need
        {
            get { return (T)base.Need; }
            set { base.Need = value; }
        }
    }
}
