﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AC.Base.Tasks;

namespace AC.Base.Forms.Windows.Tasks
{
    /// <summary>
    /// 每15分钟执行一次的自动任务周期配置界面。
    /// </summary>
    [Control(typeof(AC.Base.Tasks.Minute15TaskPeriod))]
    public class Minute15TaskPeriodConfig : Label, ITaskPeriodConfigControl
    {
        private Minute15TaskPeriod m_TaskPeriod;

        /// <summary>
        /// 每15分钟执行一次的自动任务周期配置界面。
        /// </summary>
        public Minute15TaskPeriodConfig()
        {
            this.Text = "每小时00分、15分、30分、45分启动运行，每天运行96次。";
        }

        #region ITaskPeriodConfigControl 成员

        /// <summary>
        /// 设置需配置的任务周期对象。
        /// </summary>
        /// <param name="taskPeriod">任务周期。</param>
        /// <param name="currentTaskConfig">设置周期时所配置的任务配置对象。</param>
        public void SetTaskPeriod(TaskPeriod taskPeriod, TaskConfig currentTaskConfig)
        {
            this.m_TaskPeriod = taskPeriod as Minute15TaskPeriod;
        }

        /// <summary>
        /// 获取配置的任务周期对象。
        /// </summary>
        /// <returns>任务周期。</returns>
        public TaskPeriod GetTaskPeriod()
        {
            return this.m_TaskPeriod;
        }

        #endregion
    }
}
