using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnLang.ast.lib
{
    public class FunctionalExpression : Expression
    {
        private string name;
        private List<Expression> arguments;
        public string enumFunc = string.Empty;
        public bool isEnumFunc = false;

        public FunctionalExpression(string name)
        {
            this.name = name;
            arguments = new List<Expression>();
        }

        public FunctionalExpression(string name, List<Expression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public void addArgument(Expression arg)
        {
            arguments.Add(arg);
        }

        public async Task async_eval()
        {
            int size = arguments.Count();
            Value[] values = new Value[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = arguments[i].eval();
            }

            Function function = Functions.get(name);
            var mytask = Task.Run(() => function.execute(values));
            mytask.Wait();
        }

        public Value enum_eval(Ldef function)
        {
            int size = arguments.Count();
            Value[] values = new Value[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = arguments[i].eval();
            }

            Ldef userFunction = function;
            if (size != userFunction.getArgsCount()) throw new Exception("Args count missmatch");

            Variables.push();

            for (int i = 0; i < size; i++)
            {
                Variables.set(userFunction.getArgsName(i), values[i]);
            }

            Value result = userFunction.execute(values);
            Variables.pop();
            return result;
        }

        public void void_eval()
        {
            if (Variables.isExists(enumFunc))
            {
                EnumValue enumValue = (EnumValue)Variables.get(enumFunc);
                enum_eval((Ldef)((ObjectValue)enumValue.get(name)).asObject());
                return;
            }
            int size = arguments.Count();
            Value[] values = new Value[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = arguments[i].eval();
            }

            Function function = Functions.get(name);
            if (function is UserDefineFunction)
            {
                UserDefineFunction userFunction = (UserDefineFunction)function;
                if (size != userFunction.getArgsCount()) throw new Exception("Args count missmatch");

                Variables.push();

                for (int i = 0; i < size; i++)
                {
                    Variables.set(userFunction.getArgsName(i), values[i]);
                }

                Value result = userFunction.execute(values);
                Variables.pop();
            }
            if (function is Ldef)
            {
                Ldef userFunction = (Ldef)function;
                if (size != userFunction.getArgsCount()) throw new Exception("Args count missmatch");

                Variables.push();

                for (int i = 0; i < size; i++)
                {
                    Variables.set(userFunction.getArgsName(i), values[i]);
                }

                Value result = userFunction.execute(values);
                Variables.pop();
            }
            function.execute(values);
        }

        public Value eval()
        {
            if (Variables.isExists(enumFunc))
            {
                EnumValue enumValue = (EnumValue)Variables.get(enumFunc);
                return enum_eval((Ldef)((ObjectValue)enumValue.get(name)).asObject());
            }
            int size = arguments.Count();
            Value[] values = new Value[size];
            for (int i = 0; i < size; i++)
            {
                values[i] = arguments[i].eval();
            }

            Function function = Functions.get(name);
            if (function is UserDefineFunction)
            {
                UserDefineFunction userFunction = (UserDefineFunction)function;
                if (size != userFunction.getArgsCount()) throw new Exception("Args count missmatch");

                Variables.push();

                for (int i = 0; i < size; i++)
                {
                    Variables.set(userFunction.getArgsName(i), values[i]);
                }

                Value result = userFunction.execute(values);
                Variables.pop();
                return result;
            }
            if (function is Ldef)
            {
                Ldef userFunction = (Ldef)function;
                if (size != userFunction.getArgsCount()) throw new Exception("Args count missmatch");

                Variables.push();

                for (int i = 0; i < size; i++)
                {
                    Variables.set(userFunction.getArgsName(i), values[i]);
                }

                Value result = userFunction.execute(values);
                Variables.pop();
                return result;
            }
            return function.execute(values);
        }

        public override string ToString()
        {
            return name + '(' + arguments.ToString() + ')';
        }
    }
}
