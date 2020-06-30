/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;

/**
 *
 * @author M.alkahtani
 */
import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import com.microsoft.sqlserver.jdbc.SQLServerException;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.*;
import java.lang.reflect.Method;
import java.sql.*;
import java.util.*;
import javax.swing.*;
import javax.swing.event.ListSelectionEvent;
import javax.swing.event.ListSelectionListener;
import javax.swing.table.DefaultTableModel;


public class Tests extends JFrame {
    JFrame frame = new JFrame();
    private int user_id;
    private int TEST_ID = -1;
    Vector IDs = new Vector();
    Vector<Integer> T_ID = new Vector<Integer>();
    Vector<Integer> C_T_ID = new Vector<Integer>();
    int Number_of_Tests = 0;
    public Tests(int u_id) throws SQLServerException, SQLException
    {

        Vector columnNames = new Vector();
        Vector data = new Vector();
        JButton Run = new JButton();
        this.user_id = u_id;
        try
        {
            //  Connect to the Database

            Config_Text con = new Config_Text();
            try{
                con.read();
            }catch (IOException err){


        }

         Connection DB_Con = null;

      //Statement stmt = null;
      //ResultSet rs = null;
       SQLServerDataSource ds = new SQLServerDataSource();
       ds.setUser(con.DB_user);
         ds.setPassword(con.DB_Pass);
         ds.setServerName(con.Server);
         ds.setPortNumber(Integer.parseInt(con.Port));
         ds.setDatabaseName(con.DB);
         DB_Con = ds.getConnection();





            //  Read data from a table

            Statement stmt = DB_Con.createStatement();
            String Sel = "SELECT ID, T_ID FROM dbo.C_T_R Where C_ID='" + this.user_id + "'" + "AND is_submitted ="+"'False'";
            ResultSet cursor = stmt.executeQuery(Sel);
            //ResultSetMetaData md = cursor.getMetaData();
            //int columns = md.getColumnCount();
            // Get the tests

                columnNames.addElement( "Test ID" );
                columnNames.addElement( "Test Name" );
                columnNames.addElement( "Test Description" );
                columnNames.addElement( "Test Duration" );
                

                int count = 0;
            while (cursor.next())
            {
                 Number_of_Tests = Number_of_Tests +1;
                T_ID.add(count, cursor.getInt("T_ID"));
                C_T_ID.add(count,cursor.getInt("ID"));
                count++;
                
               // IDs.addElement(T_ID);
               // IDs.addElement(C_T_ID);


                        int Test_ID = cursor.getInt("T_ID");
                        Statement stmt1 = DB_Con.createStatement();
                String Sel1 = "SELECT ID, Name, Description, Duration FROM dbo.Test Where ID ='" + Test_ID + "'";
                ResultSet rs = stmt1.executeQuery(Sel1);
               // ResultSetMetaData md = cursor.getMetaData();
               // int columns = md.getColumnCount();
                 if (rs.next())
            {
                Vector row = new Vector(4);

                for (int i = 1; i <= 4; i++)
                {
                    row.addElement( rs.getObject(i) );
                    //System.out.print(rs.getObject(i));
                }

                data.addElement( row );
            }

          //  if (cursor != null) try { cursor.close(); } catch(Exception e) {}
         //if (stmt != null) try { stmt.close(); } catch(Exception e) {}
         //       if (rs != null) try { rs.close(); } catch(Exception e) {}
        // if (stmt1 != null) try { stmt1.close(); } catch(Exception e) {}
        // if (DB_Con != null) try { DB_Con.close(); } catch(Exception e) {}
        }
            }
            //  Get column names

            

            //  Get row data

           
        catch(Exception e)
        {
            System.out.println( e );
        }

        //  Create table with database data

        final JTable table= new JTable(new DefaultTableModel(data, columnNames){
	@Override
	public boolean isCellEditable(int row, int column) {
		return false;
	}

});
table.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
ListSelectionModel tipListSelectionModel = table.getSelectionModel();

tipListSelectionModel.addListSelectionListener(new ListSelectionListener() {

public void valueChanged(ListSelectionEvent evt) {

// Ignore extra messages.

   if (evt.getValueIsAdjusting()) return;

ListSelectionModel lsm = (ListSelectionModel)evt.getSource();

if (lsm.isSelectionEmpty()) {

   System.out.println("No rows are selected.");

   }

else {

   int row = lsm.getMinSelectionIndex();
   int column = 0;
TEST_ID = Integer.parseInt(table.getValueAt(row, column).toString());
    }

   }

   });



//int row = table.getSelectedRow();



        JScrollPane scrollPane = new JScrollPane( table );
        frame.getContentPane().add( scrollPane );

        JPanel TablePanel = new JPanel();
        frame.getContentPane().add( TablePanel, BorderLayout.SOUTH );
 
        
        
        Run.setText("Run Test");
        
        Run.addActionListener(new
ActionListener()
{
public void actionPerformed(ActionEvent event)
{
Run();
}
});
        
        JPanel ButtonPanel = new JPanel();
        ButtonPanel.add(Run);
      frame.getContentPane().add( ButtonPanel, BorderLayout.SOUTH );
     /* System.out.print(T_ID);
      System.out.print(C_T_ID);
      int index = Collections.binarySearch(T_ID,1);
      System.out.print(index);*/
    }

  public int seqSearch(Vector<Integer> List, int ID){
int ret = 0;
      boolean found = false;
 


      for (int i = 0; i <= List.size(); i++){

          if (List.get(i) == ID)
           {
           ret = i;
           found = true;
           break;
           }
      }
     if (found){
          return ret;
     }
           else
              return -1;
 }

    public void Run(){
        int C_T_id = -1;
     if (TEST_ID >= 0){

         int index = seqSearch(T_ID,TEST_ID);
         if (index >= 0){
         C_T_id = C_T_ID.elementAt(index);
         //System.out.print(C_T_id);
         }
         //System.out.print(T_ID);
         //System.out.print(index);
         
         //System.out.print(C_T_ID);
         try {
              String [] str = {String.valueOf(Number_of_Tests),String.valueOf(this.user_id),String.valueOf(TEST_ID),String.valueOf(C_T_id)};
           // Tests tset = new Tests(uid);
            Class c = Class.forName("ise_test_engine.TestL");

            Method mainMethod = findMain(c);
            mainMethod.invoke(null, new Object[] { str });
            frame.setVisible(false);
         }
         catch (Throwable e) {
            System.err.println(e);
         }

   // System.out.print(TEST_ID);
        
     }else{
     System.out.print("There is no Test");
}
    }
    private static Method findMain(Class clazz)
throws Exception
{
Method[] methods = clazz.getMethods();
for (int i=0; i<methods.length; i++)
{
if (methods[i].getName().equals("main"))
return methods[i];
}
return null;
}
    public static void main(String[] args) throws SQLServerException, SQLException
    {
        //Tests f = new Tests(1);

        Tests f = new Tests(Integer.parseInt(args[0]));
        f.go();
        //f.pack();
        //f.setVisible(true);
        
    }
    public void go(){
         frame.setSize(600,600);
        frame.setDefaultCloseOperation( EXIT_ON_CLOSE );
        frame.pack();
        frame.setVisible(true);
    }
}