using System;
using System.Threading;

namespace HSP.MediaRetrieve.Business
{
    // 定义事件的参数类
    public class ValueEventArgs : EventArgs
    {
        public int Value { set; get; }
    }

    // 定义事件使用的委托
    public delegate void ValueChangedEventHandler(object sender, ValueEventArgs e);

    internal class LongTimeWork
    {
        // 定义一个事件来提示界面工作的进度
        public event ValueChangedEventHandler ValueChanged;

        // 触发事件的方法
        protected void OnValueChanged(ValueEventArgs e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        public void LongTimeMethod()
        {
            for (var i = 0; i < 100; i++)
            {
                // 进行工作
                Thread.Sleep(1000);

                // 触发事件
                var e = new ValueEventArgs {Value = i + 1};
                OnValueChanged(e);
            }
        }
    }
}