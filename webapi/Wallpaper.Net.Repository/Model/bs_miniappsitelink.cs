﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Wallpaper.Net.Repository.Model
{
    ///<summary>
    ///小程序友链表
    ///</summary>
    [SugarTable("bs_miniappsitelink")]
    public partial class bs_miniappsitelink
    {
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int MiniAppSiteLinkId { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>
        public string MiniAppId { get; set; }

        /// <summary>
        ///小程序链接
        /// </summary>
        public string MiniAppUrl { get; set; }

        /// <summary>
        /// Desc:小程序名称
        /// Default:
        /// Nullable:True
        /// </summary>
        public string MiniAppName { get; set; }

        /// <summary>
        ///跳转路径
        /// Default:
        /// Nullable:True
        /// </summary>
        public string Path { get; set; }


        /// <summary>
        /// Desc:
        /// Default:0
        /// Nullable:False
        /// </summary>
        public bool IsDeleted { get; set; }


    }
}
