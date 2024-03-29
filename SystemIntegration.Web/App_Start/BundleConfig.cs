﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace TenderInfo.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/LayoutCss").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/bootstrap-datetimepicker.min.css",
                "~/bootstrap-table-dist/bootstrap-table.min.css",
                "~/Content/font-awesome/css/font-awesome.min.css",
                "~/AdminLTE-dist/css/AdminLTE.min.css",
                "~/AdminLTE-dist/css/skins/_all-skins.min.css",
                "~/Content/toastr.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/LayoutJS").Include(
                      "~/Scripts/jquery-1.11.3.min.js",
                      "~/Scripts/jquery-form.js",//form异步提交工具
                      "~/Scripts/jquery.cookie.js",//cookie处理工具

                      //表单验证工具
                      "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.messages_cn.js",

                      "~/Scripts/bootstrap.min.js",
                      "~/bootstrap-table-dist/bootstrap-table.min.js",
                      "~/bootstrap-table-dist/locale/bootstrap-table-zh-CN.min.js",

                      "~/Scripts/moment-with-locales.js",//日期时间处理工具

                      "~/Scripts/bootstrap-datetimepicker.min.js",
                      "~/Scripts/bootstrap-datetimepicker.zh-CN.js",

                      "~/AdminLTE-dist/js/adminlte.min.js",

                      "~/Scripts/toastr.min.js",//消息提示工具
                      "~/Scripts/echarts.min.js",//图标工具
                      "~/Scripts/Ewin.js"//确认信息提示工具
                      ));
        }
    }
}