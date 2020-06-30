/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;


import java.sql.*;

/**
 *
 * @author TOSHIBA
 */

public class Connect{
     private java.sql.Connection  con = null;
     private final String url = "jdbc:sqlserver://";
     private String serverName;
     private String portNumber;
     private String databaseName;
     private String userName;
     private String password;
     // Informs the driver to use server a side-cursor,
     // which permits more than one active statement
     // on a connection.
     private final String selectMethod = "cursor";

     // Constructor
     public Connect(String SN, String PN, String DB, String UN, String Pa){
     this.serverName = SN;
     this.portNumber = PN;
     this.databaseName = DB;
     this.userName = UN;
     this.password = Pa;

     }

     private String getConnectionUrl(){
          return url+serverName+":"+portNumber+";databaseName="+databaseName+";selectMethod="+selectMethod+";";
     }

     public Connection getConnection() throws ClassNotFoundException{
         
               //Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
         try {
            Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
               this.con = DriverManager.getConnection(getConnectionUrl(),userName,password);
         } catch(java.sql.SQLException e){

         }

          return this.con;
    }
      

     /*
          Display the driver properties, database details
     */

     public void displayDbProperties(){
          java.sql.DatabaseMetaData dm = null;
          java.sql.ResultSet rs = null;
          try{
               con= this.getConnection();
               if(con!=null){
                    dm = con.getMetaData();
                    System.out.println("Driver Information");
                    System.out.println("\tDriver Name: "+ dm.getDriverName());
                    System.out.println("\tDriver Version: "+ dm.getDriverVersion ());
                    System.out.println("\nDatabase Information ");
                    System.out.println("\tDatabase Name: "+ dm.getDatabaseProductName());
                    System.out.println("\tDatabase Version: "+ dm.getDatabaseProductVersion());
                    System.out.println("Avalilable Catalogs ");
                    rs = dm.getCatalogs();
                    while(rs.next()){
                         System.out.println("\tcatalog: "+ rs.getString(1));
                    }
                    rs.close();
                    rs = null;
                    closeConnection();
               }else System.out.println("Error: No active Connection");
          }catch(Exception e){
               e.printStackTrace();
          }
          dm=null;
     }

     public void closeConnection(){
          try{
               if(this.con!=null)
                    this.con.close();
               this.con=null;
          }catch(Exception e){
               e.printStackTrace();
          }
     }
     public String Test(){
         if (this.con != null){
             return "Connected";
         }
         else if (this.con == null)
         {
             return "Connection error";
         }
 else{
             return "error";
 }
     }
   /* public static void main(String[] args) throws Exception
       {
        Connect myDbTest = new Connect();
          myDbTest.displayDbProperties();
       }*/
}