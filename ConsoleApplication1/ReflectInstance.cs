using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System;
namespace ConsoleApplication1
{
    public class ReflectInstance
    {
        public static void ValidateInstatnce<T>(T t)
        {
            var objectT = Activator.CreateInstance<T>();
            var propertyInfoList = objectT.GetType().GetProperties();

            foreach (var property in propertyInfoList)
            {
                var proValue = property.GetValue(t,null);

                var attars = property.GetCustomAttributes();
                foreach (var attar in attars)
                {
                    try
                    {
                        var validateAttar = attar as ValidationAttribute;
                        if (validateAttar!=null)
                        {
                            //Null对象绕过正则检查，不会抛错，情理之中.
                            validateAttar.Validate(proValue ,property.Name);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e is ValidationException)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public static void test()
        {
            try
            {
                var stu = new student { EName = "123" };
                ValidateInstatnce<student>(stu);
            }
            catch (Exception e)
            {
                
                throw e;
            }

        }
    }

   public class student
    {

        [RegularExpression(@"^[a-zA-Z]{1,20}$", ErrorMessage = "英文姓名必须是英文字符，且长度小于20！")]
        public string EName { get; set; }
    }
}