using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Entity;

namespace Entity
{
    /// <summary>
    /// 考试题目 
    /// </summary>
    [Table("TESTQUESTIONS")]
    public class TestQuestion : GeneralItem
    {
        public const String TypeId = "8228C905D00C44DB89D7139986FC91F1";

        public static readonly String DbTableName = "TESTQUESTIONS";

        #region 属性

        /// <summary>
        /// 标题
        /// </summary>
        [Column("TITLE")]
        public string Title
        {
            get; set;
        }

        /// <summary>
        /// 编码
        /// </summary>
        [Column("CODE")]
        public string Code
        {
            get; set;
        }

        /// <summary>
        /// 内容
        /// </summary>
        [Column("CONTENT")]
        public string Content
        {
            get; set;
        }

        /// <summary>
        /// 标准答案
        /// </summary>
        [Column("ANSWER")]
        public string Answer
        {
            get; set;
        }

        /// <summary>
        /// 选项A
        /// </summary>
        [Column("OPTIONSA")]
        public string OptionsA
        {
            get; set;
        }

        /// <summary>
        /// 选项B
        /// </summary>
        [Column("OPTIONSB")]
        public string OptionsB
        {
            get; set;
        }

        /// <summary>
        /// 选项C
        /// </summary>
        [Column("OPTIONSC")]
        public string OptionsC
        {
            get; set;
        }

        /// <summary>
        /// 选项D
        /// </summary>
        [Column("OPTIONSD")]
        public string OptionsD
        {
            get; set;
        }

        /// <summary>
        /// 判断正确项
        /// </summary>
        [Column("ANSWERYES")]
        public string AnswerYes
        {
            get; set;
        }

        /// <summary>
        /// 判断错误项
        /// </summary>
        [Column("ANWARNO")]
        public string AnwarNo
        {
            get; set;
        }


        #endregion

        #region Relationship


        #endregion
    }
}
