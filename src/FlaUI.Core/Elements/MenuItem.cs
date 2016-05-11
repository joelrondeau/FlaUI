﻿using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;
using System.Linq;
using System.Threading;
using UIA = interop.UIAutomationCore;

namespace FlaUI.Core.Elements
{
    /// <summary>
    /// Represents a menuitem which can also contain sub-menuitems
    /// </summary>
    public class MenuItem : AutomationElement
    {
        public MenuItem(Automation automation, UIA.IUIAutomationElement nativeElement) : base(automation, nativeElement) { }

        public MenuItem[] SubMenuItems
        {
            get
            {
                if (ExpandCollapsePattern != null &&
                    ExpandCollapsePattern.Current.ExpandCollapseState == ExpandCollapseState.Collapsed)
                {
                    ExpandCollapsePattern.Expand();
                    Thread.Sleep(250);
                }
                return FindAll(TreeScope.Children, ConditionFactory.ByControlType(ControlType.MenuItem)).Select(e => e.AsMenuItem()).ToArray();
            }
        }

        public InvokePattern InvokePattern
        {
            get { return PatternFactory.GetInvokePattern(); }
        }

        public ExpandCollapsePattern ExpandCollapsePattern
        {
            get { return PatternFactory.GetExpandCollapsePattern(); }
        }

        public void Invoke()
        {
            var invokePattern = InvokePattern;
            if (invokePattern != null)
            {
                invokePattern.Invoke();
            }
        }

        public void Expand()
        {
            var expandCollapsePattern = ExpandCollapsePattern;
            if (expandCollapsePattern != null)
            {
                expandCollapsePattern.Expand();
            }
        }

        public void Collapse()
        {
            var expandCollapsePattern = ExpandCollapsePattern;
            if (expandCollapsePattern != null)
            {
                expandCollapsePattern.Expand();
            }
        }
    }
}
