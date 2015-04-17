using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            ReflectInstance.test();
            //
            List < Customer > customerList= new List<Customer>();
            customerList.Add(new Customer(){OrderId="1",name="test1"});
            customerList.Add(new Customer(){OrderId="2",name="1"});
            customerList.Add(new Customer(){OrderId="3",name="ab1"});
            customerList.Add(new Customer(){OrderId="4",name="tesgtycd1"});
            var cList = customerList.Where(x=>x.name.ToUpper().Contains("GTY")).ToList();
            var clist2 = customerList.Except(cList).ToList();

            var test = customerList.GroupBy(c => c.OrderId).Select(m => m.First()).ToList();
            bool r = customerList.Count==test.Count;

            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            foreach (var c in customerList)
            {
                if (c.OrderId!=null)
                {
                    sb.Append(c.OrderId+",");
                }
            }
           sb= sb.Remove(sb.Length-1,1);
            sb.Append("}");

            var test2 = from t in customerList
                where
                    (from t1 in customerList
                        group t1 by t1.OrderId
                        into groupT
                        where groupT.Count() > 1
                         select groupT.Key).Contains(t.OrderId)
                        orderby t.OrderId
                select t;

            ImportCustomerModels rt = new ImportCustomerModels();
            rt.Channel = null;
            rt.ELastName = null;
            RegMeth<ImportCustomerModels>(rt);
            Console.ReadKey();
        }

        public static void RegMeth<T>(Object obj)
        {
            var objectT = Activator.CreateInstance<T>();
            var objectType = objectT.GetType();
            var propertyInfoList = objectType.GetProperties();

            foreach (var propertyInfo in propertyInfoList)
            {
                var attrs = propertyInfo.GetCustomAttributes(true);
                foreach (var attr in attrs)
                {
                    try
                    {
                        var validationAttr = attr as ValidationAttribute;
                        if (validationAttr != null)
                        {
                            validationAttr.Validate(propertyInfo.GetValue(obj, null), propertyInfo.Name);
                            //validationAttr.Validate(null, propertyInfo.Name);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is ValidationException)
                        {
                            Console.WriteLine("hello:\n"+e.Message);
                        }
                    }
                }

            }
        }
    }

    internal class Customer
    {
        public string OrderId { get; set; }
        public string name { get; set; }
    }


    public class ImportCustomerModels
    {

        /// <summary>
        /// 渠道号
        /// </summary>

        //[RegularExpression(@"^[1-4]$", ErrorMessage = "订单号错误！")]
        public string Channel { get; set; }

        /// <summary>
        /// 订单号,此处为string，方便捕捉Required 异常
        /// </summary>

        [Required(AllowEmptyStrings = false, ErrorMessage = "订单号不能为空！")]
        public string OrderId { get; set; }

        /// <summary>
        /// 房型
        /// </summary>

        public string CabinCategory { get; set; }

        /// <summary>
        /// 房间号
        /// </summary>
        public string CabinNo { get; set; }

        /// <summary>
        /// 英文姓
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "英文名不能为空！")]
        [RegularExpression(@"^[a-zA-Z]{1,20}$", ErrorMessage = "英文姓必须是英文字符，且长度小于20！")]
        public string ELastName { get; set; }

        /// <summary>
        /// 英文名
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "英文姓不能为空！")]
        [RegularExpression(@"^[a-zA-Z]{1,20}$", ErrorMessage = "英文名必须是英文字符，且长度小于15！")]
        public string EFirstName { get; set; }

        /// <summary>
        /// 旅客类型
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "旅客类型不能为空！")]
        [RegularExpression(@"^EF$", ErrorMessage = "目前旅客类型只支持EF！")]
        public string CustomerType { get; set; }

        /// <summary>
        /// 用餐批次
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "用餐批次不能为空！")]
        [RegularExpression(@"^[E,L]$", ErrorMessage = "目前用餐批次只支持E或L！")]
        public string DiningBatch { get; set; }

        /// <summary>
        /// 国籍代码
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "国籍代码不能为空！")]
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "国籍代码为3字母，同时在国籍表中有匹配，如CHN！")]
        public string Nationality { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "生日不能为空！")]
        [RegularExpression(@"^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$", ErrorMessage = "生日格式必须为:xxxx/xx/xx")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
          [Required(AllowEmptyStrings = false, ErrorMessage = "性别不能为空！")]
          [RegularExpression(@"^[M,F]$", ErrorMessage = "性别只支持M或F！")]
        public string Gender { get; set; }

        /// <summary>
        /// 中文名
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// 护照号 具有唯一性 可用来鉴定数据是否重复
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "护照号 为空！")]
        [RegularExpression(@"^[\da-zA-Z\.]{9}$", ErrorMessage = "护照号必须9位，且由英文+数字+“.”组成！")]
        public string PassportNo { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "发证日期不能为空！")]
        [RegularExpression(@"^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$", ErrorMessage = "发证日期格式必须为:xxxx/xx/xx！")]
        public DateTime VisaDate { get; set; }

        /// <summary>
        /// 护照有效期
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "护照有效期不能为空！")]
        [RegularExpression(@"^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$", ErrorMessage = "护照有效期格式必须为:xxxx/xx/xx！")]
        public DateTime PassportValidDate { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
          [Required(AllowEmptyStrings = false, ErrorMessage = "联系电话不能为空！")]
          [RegularExpression(@"^\d{11}$", ErrorMessage = "联系电话必须为11位数字！")]
        public string MobileNo { get; set; }

        /// <summary>
        /// 紧急联系人姓名
        /// </summary>
        public string UrgentName { get; set; }

        /// <summary>
        /// 紧急联系人电话
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "紧急联系人电话不能为空！")]
        [RegularExpression(@"^[\d-]{0,20}$", ErrorMessage = "紧急联系人电话必须由数字+‘-’组成，且不超过20字符！")]
        public string UrgentPhone { get; set; }

        /// <summary>
        /// 中途上海港口
        /// </summary>
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "中途下海港口必须3字母，且在港口表中有匹配！")]
        public string MidOnPort { get; set; }

        /// <summary>
        /// 中途下海港口
        /// </summary>
        [RegularExpression(@"^[a-zA-Z]{3}$", ErrorMessage = "中途下海港口必须3字母，且在港口表中有匹配！")]
        public string MidOffPort { get; set; }

        /// <summary>
        /// 特殊情况   
        /// </summary>
        [RegularExpression(@"^[^\u4e00-\u9fa5]+$", ErrorMessage = "特殊情况必须为不超过100字符的纯英文叙述！")]
        public string Condition { get; set; }

        /// <summary>
        /// 特殊情况详情
        /// </summary>
        [RegularExpression(@"^[^\u4e00-\u9fa5]+$", ErrorMessage = "特殊情况详情必须为不超过100字符的纯英文叙述！")]
        public string Accommodation { get; set; }

        /// <summary>
        /// 特殊服务代号
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// 特殊服务说明
        /// </summary>
        [RegularExpression(@"^[^\u4e00-\u9fa5]+$", ErrorMessage = "特殊服务说明必须为不超过100字符的纯英文叙述！")]
        public string ServiceDetails { get; set; }

        /// <summary>
        /// 特殊年数
        /// </summary>
         [RegularExpression(@"^\d{0,200}$", ErrorMessage = "特殊年数必须为0-200纯数字！")]
        public int SpecialYears { get; set; }

    }

}
