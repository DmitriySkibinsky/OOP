namespace ShopApp
{
    public interface IQueryExpression
    {
        string Interpret();
    }

    public class SelectExpression : IQueryExpression
    {
        private string _tableName;
        private List<string> _columns;

        public SelectExpression(string tableName, List<string> columns)
        {
            _tableName = tableName;
            _columns = columns;
        }

        public string Interpret()
        {
            return $"SELECT {string.Join(", ", _columns)} FROM {_tableName}";
        }
    }

    public class WhereExpression : IQueryExpression
    {
        private string _condition;

        public WhereExpression(string condition)
        {
            _condition = condition;
        }

        public string Interpret()
        {
            return $"WHERE {_condition}";
        }
    }
}