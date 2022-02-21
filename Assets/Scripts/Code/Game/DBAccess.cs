using UnityEngine;
public enum DBType
{
   
   
}
public interface DBAccess
{
    
    
}
public class DBAccessSQL : DBAccess
{

    private DataBaseSQL sql;
    public DBAccessSQL()
    {
        string connectionString = "data source = " + Application.streamingAssetsPath + "/DB.db";
        this.sql = new DataBaseSQL(connectionString);

    }

    // private void ReadUnit()
    // {
    //     unit = new SerializableDictionary<int, DBUnit>();
    //     var sqlUnit = sql.ReadFullTable(DBType.Unit.ToString());
    //     while(sqlUnit.Read())
    //     {
    //         var dBUnit = new DBUnit(sqlUnit);
    //         unit.Add(dBUnit.key, dBUnit);
    //     }
    // }

    // public DBUnit GetUnit(int key)
    // {
    //     try
    //     {
    //         return unit[key];
    //     }
    //     catch
    //     {
    //         throw new NoDataBaseKeyException(DBType.Unit,key);
    //     }
    // }

} 
public class NoDataBaseKeyException : System.Exception
{
    public NoDataBaseKeyException(DBType dbType,int key) : base($"No DataBase {dbType} Key {key}")
    {
    }
}