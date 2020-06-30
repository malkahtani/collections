/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;

/**
 *
 * @author M.alkahtani
 */
import java.sql.*;

public class connectURL {

   public static void main(String[] args) {

      // Create a variable for the connection string.
      String connectionUrl = "jdbc:sqlserver://localhost\\ISE:1433;" +
         "databaseName=ISE_TEST;user=ISE;password=password";

      // Declare the JDBC objects.
      Connection con = null;
      Statement stmt = null;
      ResultSet rs = null;

      try {
         // Establish the connection.
         Class.forName("com.microsoft.sqlserver.jdbc.SQLServerDriver");
         con = DriverManager.getConnection(connectionUrl);

         // Create and execute an SQL statement that returns some data.
         String SQL = "SELECT TOP 10 * FROM db_owner.Test";
         stmt = con.createStatement();
         rs = stmt.executeQuery(SQL);

         // Iterate through the data in the result set and display it.
         while (rs.next()) {
            System.out.println(rs.getString(4) + " " + rs.getString(6));
         }
      }

      // Handle any errors that may have occurred.
      catch (Exception e) {
         e.printStackTrace();
      }
      finally {
         if (rs != null) try { rs.close(); } catch(Exception e) {}
         if (stmt != null) try { stmt.close(); } catch(Exception e) {}
         if (con != null) try { con.close(); } catch(Exception e) {}
      }
   }
}