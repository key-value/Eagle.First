using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using Eagle.Server.Interface.Wcf;
using Eagle.Server.Services;

namespace Eagle.Cmd
{
    class Program
    {
        static void Main(string[] args)
        {

            //int maxCount = 40000;
            //List<HotSpotEntities> collection = new List<HotSpotEntities>();
            //for (int i = 0; i < maxCount; i++)
            //{
            //    Console.WriteLine(string.Format("成功创建连接对象{0}", i));
            //    var db = new HotSpotEntities();
            //    db.Connection.Open();
            //    collection.Add(db);
            //}

            Expression<Func<int, bool>> exp1 = x => x > 5;
            Expression<Func<int, bool>> exp2 = x => x < 10;

            ParameterExpression y = Expression.Parameter(typeof(int), "y");
            var newExp = new NewExpression(y);
            var newexp1 = newExp.Replace(exp1.Body);
            var newexp2 = newExp.Replace(exp2.Body);
            var newbody = Expression.And(newexp1, newexp2);

            Expression<Func<int, bool>> res = Expression.Lambda<Func<int, bool>>(newbody, y);
            //将表达式树描述的lambda表达式编译为可执行代码，并生成表示该lambda表达式的委托
            Func<int, bool> del = res.Compile();
            Console.WriteLine(del(7));
            Console.ReadLine();

        }


        public class NewExpression : ExpressionVisitor
        {
            public ParameterExpression param { get; set; }
            public NewExpression(ParameterExpression param)
            {
                this.param = param;
            }
            public Expression Replace(Expression exp)
            {
                return this.Visit(exp);
            }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                //return base.VisitParameter(node);
                return this.param;
            }
        }
    }
}
