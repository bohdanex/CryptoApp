using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace CryptoApp.Animations
{
    class GridLengthAnimation : AnimationTimeline
    {
        public IEasingFunction EasingFunction
        {
            get { return (IEasingFunction)GetValue(EasingFunctionProperty); }
            set { SetValue(EasingFunctionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EasingFunction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EasingFunctionProperty =
            DependencyProperty.Register("EasingFunction", typeof(IEasingFunction), typeof(GridLengthAnimation));

        public GridLength From
        {
            get { return (GridLength)GetValue(FromProperty);}
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(GridLength), typeof(GridLengthAnimation));

        public GridLength To
        {
            get { return (GridLength)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(GridLength), typeof(GridLengthAnimation));

        public override Type TargetPropertyType => typeof(GridLength);

        protected override Freezable CreateInstanceCore()
        {
            return new GridLengthAnimation();
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            double fromVal = ((GridLength)GetValue(GridLengthAnimation.FromProperty)).Value;
            double toVal = this.To.Value;
            if (fromVal > toVal)
            {
                if (EasingFunction != null)
                {
                    return new GridLength((1 - EasingFunction.Ease(animationClock.CurrentProgress.Value)) *
                    (fromVal - toVal) + toVal, GridUnitType.Star);
                }
                else return new GridLength((1 - animationClock.CurrentProgress.Value) *
                    (fromVal - toVal) + toVal, GridUnitType.Star);
            }
            else
            {
               if(EasingFunction != null)
                {
                    return new GridLength(EasingFunction.Ease(animationClock.CurrentProgress.Value) *
                     (toVal - fromVal) + fromVal, GridUnitType.Star);
                }
                else return new GridLength(animationClock.CurrentProgress.Value *
                     (toVal - fromVal) + fromVal, GridUnitType.Star);
            }
        }
    }
}
