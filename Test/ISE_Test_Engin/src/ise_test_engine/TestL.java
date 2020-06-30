/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;

import com.microsoft.sqlserver.jdbc.SQLServerDataSource;
import ise_test_engine.core.*;
import ise_test_engine.core.Answers;
import java.io.*;
import java.security.InvalidKeyException;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.sql.*;
import java.awt.*;
import java.awt.event.*;
import java.lang.reflect.Method;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.*;
import javax.swing.Timer;
import java.util.*;



/**
 *
 * @author M.alkahtani
 */
public class TestL implements Runnable{
        JFrame frame = new JFrame("ISE Exam");
        private Lock lock = new Lock();
	boolean next=false;
	boolean prev=false;
	int nextQuestion;
        private int currentQuestion;
	private int seconds=0;      // seconds left to complete this exam.
       
	String sTime="0:00";

	JToggleButton TF[];
        JToggleButton MCSA[];
        JToggleButton MCMA[];
        //JTextField txt_answer = new JTextField();
         JTextField FGI = new JTextField();
         JTextField FGS = new JTextField();
         JTextField MT[];
         JTextArea SA = new JTextArea(5,40);
         JTextArea EA = new JTextArea(10,40);
         boolean run = true;

    private int N_Tests;
    private int User_ID;
    private int Test_ID;
    private int C_T_ID;
    private int T_Duration;
    private int N_Q_TF,N_Q_MCSA,N_Q_MCMA,N_Q_FGS, N_Q_FGI, N_Q_MT, N_Q_SA, N_Q_EA;
    Vector<Question> Q = new Vector<Question>();
    Question ques;
    
   Vector<Answers_Log> A_L = new Vector<Answers_Log>();
    Vector<Vector> A_V = new Vector<Vector>();
    Vector<Vector> AL_V = new Vector<Vector>();
    Vector Ans = new Vector();
    Timer timer = new Timer(1000,new java.awt.event.ActionListener() {
            public void actionPerformed(ActionEvent e) {
                timer_action(e);
            }
        });
        

        BorderLayout borderLayout1 = new BorderLayout();
	FlowLayout flowLayout1 = new FlowLayout(FlowLayout.LEFT);

        JPanel panel_north = new JPanel();

        JLabel jLabel_timer = new JLabel(sTime);
	JLabel jLabel_blank = new JLabel();
        JLabel Time_to_Finish = new JLabel("Time in Min left to the end of exam: ");
        JPanel jPanel_switch = new JPanel();
        JPanel jPanel_intro = new JPanel();

        CardLayout cardLayout1 = new CardLayout();
        GridBagLayout gridBagLayout2 = new GridBagLayout();


        JButton Submit = new JButton();
        JButton Next = new JButton();
        JButton Prev = new JButton();
        JButton Start = new JButton();


       JPanel jPanel_main = new JPanel();
       GridBagLayout gridBagLayout1 = new GridBagLayout();

    JLabel jLabel_selectText = new JLabel();
    JLabel jLabel_question = new JLabel();
    JPanel jPanel_buttons = new JPanel();
    Component component1;
    JLabel jLabel_qnumber = new JLabel();
     GridBagLayout gridBagLayout3 = new GridBagLayout();
    private int value;
    Connection DB_Con = null;


     private TestL(int n_tests, int user_id, int test_id, int c_t_id) throws InvalidKeyException, NoSuchAlgorithmException, InvalidKeySpecException {
        this.N_Tests = n_tests;
        this.User_ID = user_id;
        this.Test_ID = test_id;
        this.C_T_ID = c_t_id;
        Vector<Integer> R_V = new Vector<Integer>();
        Vector<Integer> Q_t = new Vector<Integer>();
        //
        FGS.setColumns(25);
        FGI.setColumns(15);
         try {
            //  Connect to the Database

            Config_Text con = new Config_Text();
            try{
                con.read();
            }catch (IOException err){


        }

        

      
         SQLServerDataSource ds = new SQLServerDataSource();
         ds.setUser(con.DB_user);
         ds.setPassword(con.DB_Pass);
         ds.setServerName(con.Server);
         ds.setPortNumber(Integer.parseInt(con.Port));
         ds.setDatabaseName(con.DB);
         DB_Con = ds.getConnection();





            //  Read data from Database

            Statement stmt = DB_Con.createStatement();
            String Sel = "SELECT Duration, N_Q_TF, N_Q_MCSA, N_Q_MCMA, N_Q_FGS, N_Q_FGI, N_Q_MT, N_Q_SA, N_Q_EA FROM dbo.Test Where ID ='" + this.Test_ID + "'";
            ResultSet rs = stmt.executeQuery(Sel);
           
           if (rs.next()){
            this.T_Duration = rs.getInt("Duration");
            this.N_Q_TF = rs.getInt("N_Q_TF"); // 1
            this.N_Q_MCSA = rs.getInt("N_Q_MCSA"); // 2
            this.N_Q_MCMA = rs.getInt("N_Q_MCMA"); // 3
            this.N_Q_FGI = rs.getInt("N_Q_FGI");  // 5
            this.N_Q_FGS = rs.getInt("N_Q_FGS");  // 4
            this.N_Q_MT = rs.getInt("N_Q_MT");  // 6
            this.N_Q_SA = rs.getInt("N_Q_SA");  // 7
            this.N_Q_EA = rs.getInt("N_Q_EA");  // 8
            if (rs!= null) try { rs.close(); } catch(Exception e) {}
            if (stmt != null) try { stmt.close(); } catch(Exception e) {}
             }
           

            if (this.N_Q_TF > 0){
            Statement TF_stmt = DB_Con.createStatement();

            String TF_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'1'";
            ResultSet TF_rs = TF_stmt.executeQuery(TF_Sel);
            while (TF_rs.next())
            {
             R_V.addElement(TF_rs.getInt("Q_ID"));


            }
             
             if(R_V.size() >= this.N_Q_TF)
             {
                Collections.shuffle(R_V);
                for(int i=0; i < this.N_Q_TF ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }
             for(int j=0; j<Q_t.size(); j++){
             
             Statement TF1_stmt = DB_Con.createStatement();
             String TF1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet TF1_rs = TF1_stmt.executeQuery(TF1_Sel);
             
             
             
             
             if(TF1_rs.next()){
                 
             Question Que = new Question(Q_t.elementAt(j),TF1_rs.getString("Text"),1,TF1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement TFA_stmt = DB_Con.createStatement();
             String TFA_Sel = "SELECT ID,is_Right,A_Order,value,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet TFA_rs = TFA_stmt.executeQuery(TFA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(TFA_rs.next()){
                 Answers Ans = new Answers(TFA_rs.getInt("ID"),0,TFA_rs.getString("Text"),Q_t.elementAt(j),TFA_rs.getBoolean("is_Right"),TFA_rs.getInt("A_Order"),0);

              tempans.addElement(Ans);
             }
              if (TFA_rs != null) try { TFA_rs.close(); } catch(Exception e) {}
              if (TFA_stmt != null) try { TFA_stmt.close(); } catch(Exception e) {}
                 Collections.sort(tempans);
                 A_V.addElement(tempans);}
              if (TF1_rs != null) try { TF1_rs.close(); } catch(Exception e) {}
              if (TF1_stmt != null) try { TF1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (TF_rs != null) try { TF_rs.close(); } catch(Exception e) {}
           if (TF_stmt != null) try { TF_stmt.close(); } catch(Exception e) {}
             }

            // Multi Q Singl Answer
            if (this.N_Q_MCSA > 0){
            Statement MCSA_stmt = DB_Con.createStatement();

            String MCSA_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'2'";
            ResultSet MCSA_rs = MCSA_stmt.executeQuery(MCSA_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (MCSA_rs.next())
            {
             R_V.addElement(MCSA_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_MCSA)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_MCSA ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }
             for(int j=0; j<Q_t.size(); j++){

             Statement MCSA1_stmt = DB_Con.createStatement();
             String MCSA1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet MCSA1_rs = MCSA1_stmt.executeQuery(MCSA1_Sel);




             if(MCSA1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),MCSA1_rs.getString("Text"),2,MCSA1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement MCSAA_stmt = DB_Con.createStatement();
             String MCSAA_Sel = "SELECT ID,is_Right,A_Order,value,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet MCSAA_rs = MCSAA_stmt.executeQuery(MCSAA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(MCSAA_rs.next()){
                 Answers Ans = new Answers(MCSAA_rs.getInt("ID"),0,MCSAA_rs.getString("Text"),Q_t.elementAt(j),MCSAA_rs.getBoolean("is_Right"),MCSAA_rs.getInt("A_Order"),0);

              tempans.addElement(Ans);
             }
              if (MCSAA_rs != null) try { MCSAA_rs.close(); } catch(Exception e) {}
              if (MCSAA_stmt != null) try { MCSAA_stmt.close(); } catch(Exception e) {}
                Collections.sort(tempans);
                 A_V.addElement(tempans);}
              if (MCSA1_rs != null) try { MCSA1_rs.close(); } catch(Exception e) {}
              if (MCSA1_stmt != null) try { MCSA1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (MCSA_rs != null) try { MCSA_rs.close(); } catch(Exception e) {}
           if (MCSA_stmt != null) try { MCSA_stmt.close(); } catch(Exception e) {}
             }
            
            // Multi Q Multi Answer
            if (this.N_Q_MCMA > 0){
            Statement MCMA_stmt = DB_Con.createStatement();

            String MCMA_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'3'";
            ResultSet MCMA_rs = MCMA_stmt.executeQuery(MCMA_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (MCMA_rs.next())
            {
             R_V.addElement(MCMA_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_MCMA)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_MCMA ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }
                
             for(int j=0; j<Q_t.size(); j++){

             Statement MCMA1_stmt = DB_Con.createStatement();
             String MCMA1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet MCMA1_rs = MCMA1_stmt.executeQuery(MCMA1_Sel);




             if(MCMA1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),MCMA1_rs.getString("Text"),3,MCMA1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement MCMAA_stmt = DB_Con.createStatement();
             String MCMAA_Sel = "SELECT ID,is_Right,A_Order,value,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet MCMAA_rs = MCMAA_stmt.executeQuery(MCMAA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(MCMAA_rs.next()){
                 Answers Ans = new Answers(MCMAA_rs.getInt("ID"),0,MCMAA_rs.getString("Text"),Q_t.elementAt(j),MCMAA_rs.getBoolean("is_Right"),MCMAA_rs.getInt("A_Order"),0);

              tempans.addElement(Ans);
             }
              if (MCMAA_rs != null) try { MCMAA_rs.close(); } catch(Exception e) {}
              if (MCMAA_stmt != null) try { MCMAA_stmt.close(); } catch(Exception e) {}
                Collections.sort(tempans);
                 A_V.addElement(tempans);}
              if (MCMA1_rs != null) try { MCMA1_rs.close(); } catch(Exception e) {}
              if (MCMA1_stmt != null) try { MCMA1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (MCMA_rs != null) try { MCMA_rs.close(); } catch(Exception e) {}
           if (MCMA_stmt != null) try { MCMA_stmt.close(); } catch(Exception e) {}
             }

            // Fill Gap Integer
            if (this.N_Q_FGI > 0){
            Statement FGI_stmt = DB_Con.createStatement();

            String FGI_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'5'";
            ResultSet FGI_rs = FGI_stmt.executeQuery(FGI_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (FGI_rs.next())
            {
             R_V.addElement(FGI_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_FGI)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_FGI ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }

             for(int j=0; j<Q_t.size(); j++){

             Statement FGI1_stmt = DB_Con.createStatement();
             String FGI1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet FGI1_rs = FGI1_stmt.executeQuery(FGI1_Sel);




             if(FGI1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),FGI1_rs.getString("Text"),5,FGI1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement FGIA_stmt = DB_Con.createStatement();
             String FGIA_Sel = "SELECT ID,is_Right,A_Order,value,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet FGIA_rs = FGIA_stmt.executeQuery(FGIA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(FGIA_rs.next()){
                 Answers Ans = new Answers(FGIA_rs.getInt("ID"),0,"",Q_t.elementAt(j),true,0,FGIA_rs.getInt("value"));

              tempans.addElement(Ans);
             }
              if (FGIA_rs != null) try { FGIA_rs.close(); } catch(Exception e) {}
              if (FGIA_stmt != null) try { FGIA_stmt.close(); } catch(Exception e) {}
                 A_V.addElement(tempans);}
              if (FGI1_rs != null) try { FGI1_rs.close(); } catch(Exception e) {}
              if (FGI1_stmt != null) try { FGI1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (FGI_rs != null) try { FGI_rs.close(); } catch(Exception e) {}
           if (FGI_stmt != null) try { FGI_stmt.close(); } catch(Exception e) {}
             }
            // Fill Gap String
            if (this.N_Q_FGS > 0){
            Statement FGS_stmt = DB_Con.createStatement();

            String FGS_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'4'";
            ResultSet FGS_rs = FGS_stmt.executeQuery(FGS_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (FGS_rs.next())
            {
             R_V.addElement(FGS_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_FGS)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_FGS ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }

             for(int j=0; j<Q_t.size(); j++){

             Statement FGS1_stmt = DB_Con.createStatement();
             String FGS1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet FGS1_rs = FGS1_stmt.executeQuery(FGS1_Sel);




             if(FGS1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),FGS1_rs.getString("Text"),4,FGS1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement FGSA_stmt = DB_Con.createStatement();
             String FGSA_Sel = "SELECT ID,is_Right,A_Order,value,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet FGSA_rs = FGSA_stmt.executeQuery(FGSA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(FGSA_rs.next()){
                 Answers Ans = new Answers(FGSA_rs.getInt("ID"),0,FGSA_rs.getString("Text"),Q_t.elementAt(j),true,0,0);

              tempans.addElement(Ans);
             }
              if (FGSA_rs != null) try { FGSA_rs.close(); } catch(Exception e) {}
              if (FGSA_stmt != null) try { FGSA_stmt.close(); } catch(Exception e) {}
                 A_V.addElement(tempans);}
              if (FGS1_rs != null) try { FGS1_rs.close(); } catch(Exception e) {}
              if (FGS1_stmt != null) try { FGS1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (FGS_rs != null) try { FGS_rs.close(); } catch(Exception e) {}
           if (FGS_stmt != null) try { FGS_stmt.close(); } catch(Exception e) {}
             }


            // MATCH TABLE
            if (this.N_Q_MT > 0){
            Statement MT_stmt = DB_Con.createStatement();

            String MT_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'6'";
            ResultSet MT_rs = MT_stmt.executeQuery(MT_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (MT_rs.next())
            {
             R_V.addElement(MT_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_MT)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_MT ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }

             for(int j=0; j<Q_t.size(); j++){

             Statement MT1_stmt = DB_Con.createStatement();
             String MT1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet MT1_rs = MT1_stmt.executeQuery(MT1_Sel);




             if(MT1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),MT1_rs.getString("Text"),6,MT1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement MTA_stmt = DB_Con.createStatement();
             String MTA_Sel = "SELECT ID,PID,A_Order,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet MTA_rs = MTA_stmt.executeQuery(MTA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(MTA_rs.next()){
                 Answers Ans = new Answers(MTA_rs.getInt("ID"),MTA_rs.getInt("PID"),MTA_rs.getString("Text"),Q_t.elementAt(j),true,MTA_rs.getInt("A_Order"),0);

              tempans.addElement(Ans);
             }
              if (MTA_rs != null) try { MTA_rs.close(); } catch(Exception e) {}
              if (MTA_stmt != null) try { MTA_stmt.close(); } catch(Exception e) {}
                    Collections.sort(tempans);
                    A_V.addElement(tempans);}
              if (MT1_rs != null) try { MT1_rs.close(); } catch(Exception e) {}
              if (MT1_stmt != null) try { MT1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (MT_rs != null) try { MT_rs.close(); } catch(Exception e) {}
           if (MT_stmt != null) try { MT_stmt.close(); } catch(Exception e) {}
             }
             // Short Answer
            if (this.N_Q_SA > 0){
            Statement SA_stmt = DB_Con.createStatement();

            String SA_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'7'";
            ResultSet SA_rs = SA_stmt.executeQuery(SA_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (SA_rs.next())
            {
             R_V.addElement(SA_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_SA)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_SA ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }

             for(int j=0; j<Q_t.size(); j++){

             Statement SA1_stmt = DB_Con.createStatement();
             String SA1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet SA1_rs = SA1_stmt.executeQuery(SA1_Sel);




             if(SA1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),SA1_rs.getString("Text"),7,SA1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement SAA_stmt = DB_Con.createStatement();
             String SAA_Sel = "SELECT ID,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet SAA_rs = SAA_stmt.executeQuery(SAA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(SAA_rs.next()){
                 Answers Ans = new Answers(SAA_rs.getInt("ID"),0,SAA_rs.getString("Text"),Q_t.elementAt(j),true,0,0);

              tempans.addElement(Ans);
             }
              if (SAA_rs != null) try { SAA_rs.close(); } catch(Exception e) {}
              if (SAA_stmt != null) try { SAA_stmt.close(); } catch(Exception e) {}
                 A_V.addElement(tempans);}
              if (SA1_rs != null) try { SA1_rs.close(); } catch(Exception e) {}
              if (SA1_stmt != null) try { SA1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (SA_rs != null) try { SA_rs.close(); } catch(Exception e) {}
           if (SA_stmt != null) try { SA_stmt.close(); } catch(Exception e) {}
             }

            // Eassy Answer
            if (this.N_Q_EA > 0){
            Statement EA_stmt = DB_Con.createStatement();

            String EA_Sel = "SELECT Q_ID FROM dbo.Q_T_R Where T_ID ='" + this.Test_ID + "'" + "AND Q_Type ="+"'8'";
            ResultSet EA_rs = EA_stmt.executeQuery(EA_Sel);
            R_V.removeAllElements();
            Q_t.removeAllElements();
            while (EA_rs.next())
            {
             R_V.addElement(EA_rs.getInt("Q_ID"));


            }

             if(R_V.size() >= this.N_Q_EA)
             {
                Collections.shuffle(R_V);

                for(int i=0; i < this.N_Q_EA ;i++){
                  Q_t.addElement(R_V.elementAt(i)) ;
             }

             for(int j=0; j<Q_t.size(); j++){

             Statement EA1_stmt = DB_Con.createStatement();
             String EA1_Sel = "SELECT Wight,Text FROM dbo.Questions Where ID='" + Q_t.elementAt(j) + "'";
             ResultSet EA1_rs = EA1_stmt.executeQuery(EA1_Sel);




             if(EA1_rs.next()){

             Question Que = new Question(Q_t.elementAt(j),EA1_rs.getString("Text"),8,EA1_rs.getFloat("Wight"));
             Q.addElement(Que);
             Statement EAA_stmt = DB_Con.createStatement();
             String EAA_Sel = "SELECT ID,Text FROM dbo.Answers Where Q_ID='" + Q_t.elementAt(j) + "'";
             ResultSet EAA_rs = EAA_stmt.executeQuery(EAA_Sel);
             Vector<Answers> tempans = new Vector<Answers>();

             while(EAA_rs.next()){
                 Answers Ans = new Answers(EAA_rs.getInt("ID"),0,EAA_rs.getString("Text"),Q_t.elementAt(j),true,0,0);

              tempans.addElement(Ans);
             }
              if (EAA_rs != null) try { EAA_rs.close(); } catch(Exception e) {}
              if (EAA_stmt != null) try { EAA_stmt.close(); } catch(Exception e) {}
                 A_V.addElement(tempans);}
              if (EA1_rs != null) try { EA1_rs.close(); } catch(Exception e) {}
              if (EA1_stmt != null) try { EA1_stmt.close(); } catch(Exception e) {}

                }

             }
           if (EA_rs != null) try { EA_rs.close(); } catch(Exception e) {}
           if (EA_stmt != null) try { EA_stmt.close(); } catch(Exception e) {}
             }


    } catch(Exception e)
        {
            System.out.println( e );
        }
       
 /* for(int c=0; c<Q.size(); c++){
    Question curValue = Q.elementAt(c);
    System.out.print(curValue.toString());
  }
  System.out.println();
for(int i1=0; i1<A_V.size(); i1++){
  Vector temp = (Vector)A_V.elementAt(i1);
  for(int c1=0; c1<temp.size(); c1++){
    Answers curValue = (Answers)temp.elementAt(c1);
    System.out.print(curValue.get_pid());
  }
  System.out.println();
}*/

        component1 = Box.createHorizontalStrut(8);
        frame.getContentPane().setLayout(borderLayout1);
        panel_north.setLayout(flowLayout1);


        
        jPanel_switch.setLayout(cardLayout1);
        jPanel_intro.setLayout(gridBagLayout2);
        
        Prev.setText("Prev");
        Prev.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(ActionEvent e) {
                Prev_action(e);
            }

           
        });
        Next.setText("Next");
        Next.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(ActionEvent e) {
                try {
                    Next_action(e);
                } catch (SQLException ex) {
                    Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                }
            }


        });
         Submit.setText("Submit");
        Submit.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(ActionEvent e) {
                
                    try {
                        int n = JOptionPane.showConfirmDialog(
    frame,
    "Are you sure that you want to submit this exam?",
    "Submit Exam!",
    JOptionPane.YES_NO_OPTION);

if (n == JOptionPane.YES_OPTION) {
                try {
                    Submit();
                } catch (IOException ex) {
                    Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                }


}



                        
                    
                    
                } catch (SQLException ex) {
                    Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                }
            }


        });
        Start.setText("Start");
        Start.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(ActionEvent e) {
                try {
                    Start_action(e);
                } catch (SQLException ex) {
                    Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                }
            }


        });
        jPanel_main.setLayout(gridBagLayout1);
        jLabel_timer.setAlignmentY((float) 0.0);
        panel_north.setMinimumSize(new Dimension(39, 20));
        Time_to_Finish.setForeground(Color.red);
        jLabel_timer.setForeground(Color.red);
        
        panel_north.add(Time_to_Finish,null);
        panel_north.add(jLabel_timer, null);
        panel_north.add(component1, null);
        panel_north.add(jLabel_qnumber, null);


        frame.getContentPane().add(panel_north, BorderLayout.NORTH);
        frame.getContentPane().add(jPanel_switch, BorderLayout.CENTER);

        jPanel_switch.add(jPanel_intro, "intero");
        jPanel_switch.add(jPanel_main, "main");
        cardLayout1.show(jPanel_switch,"intero");

        jPanel_buttons.setLayout(new FlowLayout(FlowLayout.CENTER));
		
		jPanel_buttons.add(Prev);
                jPanel_buttons.add(Next);
		jPanel_buttons.add(Submit);

		disable_buttons();
//System.out.print(Q);
    
		seconds = this.T_Duration*60;

                jPanel_intro.add(Start);

    }

     public void run() {
		while(!Thread.interrupted())
		{
			synchronized (lock) {
				if(lock.isLocked())
				{
					try {
						wait();
					}
					catch (InterruptedException ex) {
						ex.printStackTrace();
					}
					lock.setLock(false);
				}
				if(seconds == 0)
				{
                    try {
                        try {
                            Submit();
                        } catch (IOException ex) {
                            Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                        }
                    } catch (SQLException ex) {
                        Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                    }
                        
                    
				}
				if(next)
				{
					next();
				}
				else if(prev)
				{
					prev();
				}
				else if(currentQuestion != nextQuestion){
				     switch_question(nextQuestion);
				}
				jLabel_timer.setText(sTime);
				try {
					lock.notifyAll();
					lock.wait();
				}
				catch (InterruptedException ex) {
					ex.printStackTrace();
				}
			}
		}
	}

     public static void main(String[] args) throws InvalidKeyException, NoSuchAlgorithmException, InvalidKeySpecException
    {
       /* System.out.print("Number_of_Tests = ");
        System.out.print(Integer.parseInt(args[0]));
        System.out.print("\n");
        System.out.print("User_ID = ");
        System.out.print(Integer.parseInt(args[1]));
        System.out.print("\n");
        System.out.print("Test_ID = ");
        System.out.print(Integer.parseInt(args[2]));
        System.out.print("\n");
        System.out.print("Can_Test_ID = ");
        System.out.print(Integer.parseInt(args[3]));
        TestL Tester = new TestL(Integer.parseInt(args[0]),Integer.parseInt(args[1]),Integer.parseInt(args[2]),Integer.parseInt(args[3]));

*

        */
          TestL Tester = new TestL(Integer.parseInt(args[0]),Integer.parseInt(args[1]),Integer.parseInt(args[2]),Integer.parseInt(args[3]));
   // TestL Tester = new TestL(1,1,1,1);
    Tester.go();
	}

	private void go()
	{
		//frame.setExtendedState(Frame.MAXIMIZED_BOTH);
                //frame.setUndecorated(true);
            frame.setSize(600,600);
		frame.setVisible(true);
	}


   


    private void disable_buttons() {
        this.Next.setEnabled(false);
        this.Prev.setEnabled(false);
        this.Submit.setEnabled(false);
    }

    private void Submit() throws SQLException, IOException {
Writer out = new OutputStreamWriter(new FileOutputStream("SQL.sql"));
   
        timer.stop();
        float QW,SAW,TW;
        float UT=0;
        int N_Q_N_M = 0;
        int N_Q_N_R = 0;
        int N_Q_M = 0;
        Vector temp1= new Vector();
        //Vector<ArrayList> wight = new Vector<ArrayList>();

  TW=0;
  for (int qi=0; qi<Q.size();qi++){
      TW = TW + Q.elementAt(qi).get_wight();
  }
//System.out.println(AL_V.size());
for(int i1=0; i1<AL_V.size(); i1++){
  Vector temp = (Vector)AL_V.elementAt(i1);

  ArrayList myarr = new ArrayList();
  myarr.add(i1);
  QW = Q.elementAt(i1).get_wight();
   myarr.add(QW);
  //TW = TW + QW;
  //SAW = QW;
  temp1 = (Vector) A_V.elementAt(i1);



  if (Q.elementAt(i1).get_type()==6){

      int size = temp1.size();
      SAW = QW / (size/2);
     // myarr.add(SAW);
  }else if(Q.elementAt(i1).get_type()==3){
      //Answers an = (Answers) A_V.elementAt(i1);
      int count = 0;
      for (int t=0; t<temp1.size();t++){
         Answers ans = (Answers) temp1.get(t);
         if(ans.get_right()== true){
             count++;
         }
      }
      SAW = QW/count;
     // myarr.add(SAW);

  }else{
     SAW = QW;
  }

  for(int c1=0; c1<temp.size(); c1++){
    Answers_Log curValue = (Answers_Log)temp.elementAt(c1);
    //System.out.print(curValue.get_is_right());
    if (curValue.get_is_right() == true){
        UT = UT + SAW;
    }
    if (curValue.get_right() == true){
        N_Q_M++;
    }else{
        N_Q_N_M++;
    }
    if (curValue.get_need_rev() == true){
        N_Q_N_R++;
    }
    // Ins values in DB
    try {
    Statement st = DB_Con.createStatement();
    String st_str = "INSERT INTO dbo.Answers_Log (C_T_R_ID_A, Q_ID, A_ID, Text, is_marked, need_rev, PID, Date_Submitted, value, is_right, is_wrong, is_answered) VALUES('" + curValue.get_C_T_R_id() + "','" + curValue.get_qid() + "','" + curValue.get_aid() + "','" + curValue.get_text() + "','" + curValue.get_right() + "','" + curValue.get_need_rev() + "','" + curValue.get_pid() + "','" + curValue.get_date() + "','" + curValue.get_Vaule() + "','" + curValue.get_is_right() + "','" + curValue.get_is_wronge() + "','"  + curValue.get_not_answered() + "')" ;
    st.executeUpdate(st_str);
      } catch (SQLException sqlex) {
   
        N_Q_M=0;
        N_Q_N_M=0;
        N_Q_N_R=0;
        SAW=0;
        QW=0;
        UT=0;
        File SQL = new File("SQL.sql");
                   
                        for(int i11=0; i11<AL_V.size(); i11++){
  Vector temp2 = (Vector)AL_V.elementAt(i11);

  //ArrayList myarr = new ArrayList();
  //myarr.add(i1);
  QW = Q.elementAt(i11).get_wight();
   //myarr.add(QW);
  //TW = TW + QW;
  //SAW = QW;
  temp1 = (Vector) A_V.elementAt(i11);



  if (Q.elementAt(i11).get_type()==6){

      int size = temp1.size();
      SAW = QW / (size/2);
     // myarr.add(SAW);
  }else if(Q.elementAt(i11).get_type()==3){
      //Answers an = (Answers) A_V.elementAt(i1);
      int count = 0;
      for (int t=0; t<temp1.size();t++){
         Answers ans = (Answers) temp1.get(t);
         if(ans.get_right()== true){
             count++;
         }
      }
      SAW = QW/count;
     // myarr.add(SAW);

  }else{
     SAW = QW;
  }

  for(int c11=0; c11<temp2.size(); c11++){
    Answers_Log curValue1 = (Answers_Log)temp2.elementAt(c11);
    //System.out.print(curValue.get_is_right());
    if (curValue1.get_is_right() == true){
        UT = UT + SAW;
    }
    if (curValue1.get_right() == true){
        N_Q_M++;
    }else{
        N_Q_N_M++;
    }
    if (curValue1.get_need_rev() == true){
        N_Q_N_R++;
    }
                        /*
                        System.out.println("Temp Size : ");
                        System.out.println(temp.size());
                        for(int cc1=0; cc1<temp.size(); cc1++){
                         Answers_Log curValue1 = (Answers_Log)temp.elementAt(cc1);
                        */
                        String st_str = "INSERT INTO dbo.Answers_Log (C_T_R_ID_A, Q_ID, A_ID, Text, is_marked, need_rev, PID, Date_Submitted, value, is_right, is_wrong, is_answered) VALUES('" + curValue1.get_C_T_R_id() + "','" + curValue1.get_qid() + "','" + curValue1.get_aid() + "','" + curValue1.get_text() + "','" + curValue1.get_right() + "','" + curValue1.get_need_rev() + "','" + curValue1.get_pid() + "','" + curValue1.get_date() + "','" + curValue1.get_Vaule() + "','" + curValue1.get_is_right() + "','" + curValue1.get_is_wronge() + "','"  + curValue1.get_not_answered() + "')" ;

                           out.write(st_str);

                        out.write(System.getProperty("line.separator"));
                            }
                        
                    } 
      }
    System.out.print("Date: ");
        System.out.print(curValue.get_date());
        System.out.println();
  }
 // System.out.println();

        }
    try{


    Statement st1 = DB_Con.createStatement();
    String st_str1 = "INSERT INTO dbo.Temp_Result (C_T_R_ID, Total_Marks, User_Marks, N_Q_N_R, N_Q_N_M, N_Q_M, Result) VALUES('" + this.C_T_ID + "','" + TW + "','" + UT + "','" + N_Q_N_R + "','" + N_Q_N_M + "','" + N_Q_M + "','" + ((UT/TW)*100) + "')" ;
    st1.executeUpdate(st_str1);
    Statement update = DB_Con.createStatement();
    String st_str2 = "UPDATE dbo.C_T_R SET is_submitted = 'true' WHERE ID =" + this.C_T_ID ;

    


    update.executeUpdate(st_str2);
        }catch (SQLException sqlex1) {
    try {
                        
                        String st_str1 = "INSERT INTO dbo.Temp_Result (C_T_R_ID, Total_Marks, User_Marks, N_Q_N_R, N_Q_N_M, N_Q_M, Result) VALUES('" + this.C_T_ID + "','" + TW + "','" + UT + "','" + N_Q_N_R + "','" + N_Q_N_M + "','" + N_Q_M + "','" + ((UT/TW)*100) + "')" ;
                        
                        String st_str2 = "UPDATE dbo.C_T_R SET is_submitted = 'true' WHERE ID =" + this.C_T_ID ;
                        out.write(st_str1);
                        out.write(System.getProperty("line.separator"));
                        out.write(st_str2);
                        out.write(System.getProperty("line.separator"));
    } finally {
      out.close();
      System.exit(0);
    }
    
                    
        }
        System.out.print("User Marks: ");
        System.out.print(UT);
        System.out.println();
        System.out.print("User %: ");
        System.out.print(((UT/TW)*100));
        System.out.println();
        System.out.print("Total Marks: ");
        System.out.print(TW);
        System.out.println();
if (this.N_Tests > 1){
    try {
              String [] str = {String.valueOf(this.User_ID)};
           // Tests tset = new Tests(uid);
            Class c = Class.forName("ise_test_engine.Tests");

            Method mainMethod = findMain(c);
            mainMethod.invoke(null, new Object[] { str });
            frame.setVisible(false);
         }
         catch (Throwable e) {
            System.err.println(e);
         }
}else{
            System.exit(0);
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
    private void next() {
       synchronized (lock) {
			switch_question(nextQuestion);
			next=false;
                        
		}
    }

    private void prev() {
        synchronized (lock) {
			switch_question(nextQuestion);
			prev=false;
		}
    }

    private void switch_question(int nextQuestion) {
        currentQuestion = nextQuestion;
        
		show_current_question();
                
    }

    void show_current_question()
	{
		try
		{
                    
			GridBagConstraints gc = new GridBagConstraints();
			gc.gridx=0; gc.gridy=0;
			gc.anchor = gc.WEST;

			jPanel_main.removeAll();

			jLabel_qnumber.setText("Question No: "+Integer.toString(currentQuestion)+" Of "+ Q.size());

			ques =  Q.get(currentQuestion-1);
                        Ans = A_V.get(currentQuestion-1);
                        run = false;
			jLabel_question.setText("<HTML>"+ques.toString());
			jPanel_main.add(jLabel_question,gc);
			gc.gridy++;

			if (ques.get_type() == 1)
                        {

                            // TF Q
                            
                            //jPanel_main.add(new JLabel("Select 5"),gc);

					TF = new JCheckBox[Ans.size()];
                                         ButtonGroup buttonGroup1 = new ButtonGroup();
					for(int i=0 ; i < Ans.size() ; i++)
					{
						Answers ans= (Answers) Ans.get(i);
						TF[i] = (JCheckBox) new JCheckBox();
						TF[i].setText("<HTML>"+ans.get_text());
                                                buttonGroup1.add(TF[i]);
						jPanel_main.add(TF[i],gc);
						gc.gridy++;
					}
                            jPanel_main.add(jPanel_buttons,gc);
                            jPanel_main.validate();
                            jPanel_main.repaint();
                            //Ans.removeAllElements();
                            
                    }
                            //MCSA Q
                           if (ques.get_type() == 2){

                            //System.out.print("2");
                               MCSA = new JRadioButton[Ans.size()];
                               ButtonGroup buttonGroup1 = new ButtonGroup();
					for(int i=0 ; i < Ans.size() ; i++)
					{
						Answers ans= (Answers) Ans.get(i);
						MCSA[i] = new JRadioButton();
						MCSA[i].setText("<HTML>"+ans.get_text());
                                                buttonGroup1.add(MCSA[i]);
						jPanel_main.add(MCSA[i],gc);
						gc.gridy++;
					}
                            
                            jPanel_main.add(jPanel_buttons,gc);
                            jPanel_main.validate();
                            jPanel_main.repaint();
                            //Ans.removeAllElements();
                           
                    }

                        // MSMA Q
                        if (ques.get_type() == 3){
                            int count = 0;
                            for (int j=0; j<Ans.size();j++){
                                Answers ans1 = (Answers) Ans.get(j);
                                if (ans1.get_right()== true){
                                    count++;
                                }
                            }
                            jPanel_main.add(new JLabel("Select only "+count+" answers"),gc);
                            gc.gridy++;
                            MCMA = new JRadioButton[Ans.size()];

					for(int i=0 ; i < Ans.size() ; i++)
					{
						Answers ans= (Answers) Ans.get(i);
						MCMA[i] = new JRadioButton();
						MCMA[i].setText("<HTML>"+ans.get_text());
                                                
						jPanel_main.add(MCMA[i],gc);
						gc.gridy++;
					}

                            jPanel_main.add(jPanel_buttons,gc);
                            jPanel_main.validate();
                            jPanel_main.repaint();
                            //Ans.removeAllElements();
                            
                    }

                            //FGS Q
                            if (ques.get_type() == 4) {

                                jPanel_main.add(new JLabel("Enter your answer below"),gc);
 				 gc.gridy++;
                                 FGS.setText("");
				 jPanel_main.add(FGS,gc);
 				 gc.gridy++;
                                 jPanel_main.add(jPanel_buttons,gc);
                                 jPanel_main.validate();
                                 jPanel_main.repaint();
                                 //Ans.removeAllElements();
                                 
                    }
                            //FGI Q
                            if (ques.get_type() == 5){

				 jPanel_main.add(new JLabel("Enter your answer below"),gc);
 				 gc.gridy++;
                                 FGI.setText("");
				 jPanel_main.add(FGI,gc);
 				 gc.gridy++;
                                 jPanel_main.add(jPanel_buttons,gc);
                                 jPanel_main.validate();
                                 jPanel_main.repaint();
                                 //Ans.removeAllElements();
                                 
                    }
                            //MT Q
                            if (ques.get_type() == 6){

                           // jPanel_main.add(new JLabel("Enter the number of the left column statment in the box"),gc);
 				 gc.gridy++;
                                 MT = new JTextField[(Ans.size()/2)];
                                 int count = 0;
                                 int lcount = 0;
                                 char[] letters = {'A','B','C','D','E','F','G','H','I'};
					for(int i=0 ; i < Ans.size() ; i++)
					{
						Answers ans= (Answers) Ans.get(i);

                                                if (ans.get_pid()== 0){

                                                 jPanel_main.add(new JLabel("<HTML>"+String.valueOf(count+1)+") " +ans.get_text()),gc);
                                                 //gc.gridx++;
                                                 //JLabel l = new JLabel(ans.get_text());
                                                 //jPanel_main.add(l,gc);
                                                 jPanel_main.add(new JLabel("      "),gc);
                                                 gc.gridx++;
						MT[count] = (JTextField) new JTextField();
                                                MT[count].setColumns(5);
						MT[count].setText("");

                                                jPanel_main.add(MT[count],gc);
                                                jPanel_main.add(new JLabel("      "),gc);
                                                gc.gridx++;
                                                
                                                count++;
                                            } else{
                                                    jPanel_main.add(new JLabel(letters[lcount]+") "),gc);
                                                    
                                                    gc.gridx++;
                                                    JLabel l = new JLabel("<HTML>"+ans.get_text());
                                                    jPanel_main.add(l,gc);

                                                    gc.gridy++;

                                                    gc.gridx = 0;
                                                    lcount++;

                                            }



					}
                                 gc.gridy++;
                            jPanel_main.add(jPanel_buttons,gc);
                            jPanel_main.validate();
                            jPanel_main.repaint();


                                 //Ans.removeAllElements();

                    }



                            //SA Q
                            if (ques.get_type() == 7){
                             jPanel_main.add(new JLabel("Enter your answer below"),gc);
 				 gc.gridy++;
                                 SA.setText("");
                                 JScrollPane scrollPane = new JScrollPane(SA);
                                 scrollPane.setPreferredSize(new Dimension(250, 250));
				 jPanel_main.add(scrollPane,gc);
 				 gc.gridy++;
                                 jPanel_main.add(jPanel_buttons,gc);
                                 jPanel_main.validate();
                                 jPanel_main.repaint();
                                 //Ans.removeAllElements();
                                 
                    }
                            //EA Q
                           if (ques.get_type() == 8){
                             jPanel_main.add(new JLabel("Enter your answer below"),gc);
 				 gc.gridy++;
                                  EA.setText("");
                                 JScrollPane scrollPane = new JScrollPane(EA);
                                 scrollPane.setPreferredSize(new Dimension(250, 250));
				 jPanel_main.add(scrollPane,gc);
 				 gc.gridy++;
                                 jPanel_main.add(jPanel_buttons,gc);
                                 jPanel_main.validate();
                                 jPanel_main.repaint();
                                 //Ans.removeAllElements();
                                 
                    }
			
		}catch(Exception ex) {
			ex.printStackTrace();
		}
	}
    
    public void Prev_action(ActionEvent e) {
        
                if(currentQuestion != 1)
		{
		    nextQuestion--;
                    
		    prev=true;
		}
               
            }
     private void Next_action(ActionEvent e) throws SQLException {

       if(currentQuestion != Q.size())
		{
        

                   if(run==false){
                       
                     Question question =  Q.get(currentQuestion-1);
                    

                       // TF Q
                 if (question.get_type() == 1)
                        {

                           
                     

                     boolean non_selecte = true;
                    
                            for (int i = 0; i<TF.length ;i++){
                                
                                Answers ans = (Answers) Ans.get(i);
                                if(TF[i].isSelected()){
                                   

                                   non_selecte = false;
                                 if (ans.get_right() == true){
                                   // The user gave the right answer
                                   Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,true,false,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);
                                    
                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                
                                
                                 } else {
                                      // The user gave the wrong answer
                                       Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,false,true,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);
                                    if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                        
                                    
                                       
                                }

                                 }
                                }
                            
                     if (non_selecte == true){
                         // No answer is given
                         Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,0,false,false,true);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);
                                     
                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                         
                     }

                 }
                         
                 
                            

                   

                //MCSA Q
                           if (question.get_type() == 2){

                            boolean non_selecte = true;

                            for (int i = 0; i<MCSA.length ;i++){
                                //System.out.print("Entered");
                                Answers ans = (Answers) Ans.get(i);
                                if(MCSA[i].isSelected()){
                                
                                    non_selecte = false;
                                 if (ans.get_right() == true){

                                     // The user gave the right answer
                                Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,true,false,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                
                                 } else {
                                        // The user gave the wrong answer
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,false,true,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                        
                                }

                                 }
                                }

                     if (non_selecte == true){
                         // No answer is given
                         Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",question.get_id(),true,false,0,false,false,true);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                     }
                            

                    }

                        // MSMA Q
                        if (question.get_type() == 3){

                            boolean non_selecte = true;
                            Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                            for (int i = 0; i<MCMA.length ;i++){
                                
                                Answers ans = (Answers) Ans.get(i);
                                if(MCMA[i].isSelected()){
                               
                                    non_selecte = false;
                                 if (ans.get_right() == true){
                                     // The user gave the right answer
                                 Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 } else {
                                        // The user gave the wrong answer
                                    
                                        Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                }

                                 }
                                }

                     if (non_selecte == true){
                         // No answer is given
                        Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",question.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                     }
                            

                    }

                            //FGS Q
                            if (question.get_type() == 4) {
                                boolean right = false;
                                String answer = FGS.getText();
                                Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                if (answer.isEmpty()){
                                    right = true;
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,question.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }else{

                                for (int i = 0; i<Ans.size() ;i++){
                                Answers ans = (Answers) Ans.get(i);
                                 
                                 
                                 if (answer.equalsIgnoreCase(ans.get_text())){
                                     right = true;
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }


                                }
                                

                                


                    }
                                if(!right){
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,question.get_id(),true,true,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                }
                       }

                            //FGI Q
                            if (question.get_type() == 5){
                                Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                boolean right = false;
                                try {
                                   int value1 = Integer.parseInt(FGI.getText());
                                   System.out.println(value1);
                                    for (int i = 0; i<Ans.size() ;i++){
                                Answers ans = (Answers) Ans.get(i);

                                 if (value1 == ans.get_Vaule()){
                                     right = true;
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,value1,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }
                                    }
                                   if (!right){
                                       Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",question.get_id(),true,false,value1,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                   }
                                    } catch(NumberFormatException nex) {
                                        Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",question.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                    }

                              
                                


                    }
                            //MT Q
                            if (question.get_type() == 6){
                                boolean empty = false;
                                int count = 0;
                                Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();

                                for (int i=0; i<MT.length; i++){
                                  try {
                                      value = Integer.parseInt(MT[i].getText());
                                      } catch(NumberFormatException nex) {
                                          empty = true;
                                      Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",question.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                    }
                                  count++;

                                
                                   for (int j = 0; j<Ans.size() ;j++){
                                    Answers ans = (Answers) Ans.get(j);
                                    
                                  if (ans.get_pid()> 0){
                                      if(count==ans.get_order()){
                                         
                                          
                                         for (int l=0; l<Ans.size();l++){
                                             Answers ans1 = (Answers) Ans.get(l);
                                            
                                             if (ans1.get_pid()==0){
                                                
                                                 if (ans1.get_order()==value){
                                                

                                                     if(ans.get_pid()== ans1.get_id()){
                                                    if(!empty){
                                                         Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),ans1.get_id(),"",question.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                                     
                                                         }
                                                     } else {
                                                         
                                                       if(!empty){
                                                           Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),ans1.get_id(),"",question.get_id(),true,false,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                                         
                                                         }
                                                     }
                                                     break;
                                                     

                                                 }
                                                
                                                     }


                                         }
                                      break;
                                      }


                                      
                                  }
                                }



                                 
                                    }

                           
                            

                    }

                            //SA Q
                            if (question.get_type() == 7){
                           String answer = SA.getText();
                           boolean right = false;
                           Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                if (answer.isEmpty()){
                                    right = true;
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,question.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }else{
                                  for (int i = 0; i<Ans.size() ;i++){
                                Answers ans = (Answers) Ans.get(i);

                                 if (answer.equalsIgnoreCase(ans.get_text())){
                                    right = true;
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",question.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }
                                }
                                  if(!right){
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,question.get_id(),true,true,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                }
                            

                    }
                       }
                            //EA Q
                           if (question.get_type() == 8){

                            String answer = EA.getText();

                            Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,question.get_id(),false,false,0,false,false,false);
                            Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                
                           
                            
                    }
         

                }
			nextQuestion++;
                        
			next =true;
		
                   }

		else
		{
                    // last Q
            // TF Q
                 if (ques.get_type() == 1)
                        {




                     boolean non_selecte = true;

                            for (int i = 0; i<TF.length ;i++){

                                Answers ans = (Answers) Ans.get(i);
                                if(TF[i].isSelected()){


                                   non_selecte = false;
                                 if (ans.get_right() == true){
                                   // The user gave the right answer
                                   Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,true,false,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }


                                 } else {
                                      // The user gave the wrong answer
                                       Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,false,true,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);
                                    if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }



                                }

                                 }
                                }

                     if (non_selecte == true){
                         // No answer is given
                         Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,0,false,false,true);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                     }

                 }






                //MCSA Q
                           if (ques.get_type() == 2){

                            boolean non_selecte = true;

                            for (int i = 0; i<MCSA.length ;i++){
                                //System.out.print("Entered");
                                Answers ans = (Answers) Ans.get(i);
                                if(MCSA[i].isSelected()){

                                    non_selecte = false;
                                 if (ans.get_right() == true){

                                     // The user gave the right answer
                                Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,true,false,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }


                                 } else {
                                        // The user gave the wrong answer
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,false,true,false);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }


                                }

                                 }
                                }

                     if (non_selecte == true){
                         // No answer is given
                         Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,0,false,false,true);
                                   Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                     }


                    }

                        // MSMA Q
                        if (ques.get_type() == 3){

                            boolean non_selecte = true;
                            Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                            for (int i = 0; i<MCMA.length ;i++){

                                Answers ans = (Answers) Ans.get(i);
                                if(MCMA[i].isSelected()){

                                    non_selecte = false;
                                 if (ans.get_right() == true){
                                     // The user gave the right answer
                                 Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 } else {
                                        // The user gave the wrong answer

                                        Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                }

                                 }
                                }

                     if (non_selecte == true){
                         // No answer is given
                        Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                     }


                    }

                            //FGS Q
                            if (ques.get_type() == 4) {
                                boolean right = false;
                                String answer = FGS.getText();
                                Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                if (answer.isEmpty()){
                                    right = true;
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,ques.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }else{

                                for (int i = 0; i<Ans.size() ;i++){
                                Answers ans = (Answers) Ans.get(i);


                                 if (answer.equalsIgnoreCase(ans.get_text())){
                                     right = true;
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }


                                }





                    }
                                if(!right){
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,ques.get_id(),true,true,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                }
                       }

                            //FGI Q
                            if (ques.get_type() == 5){
                                Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                boolean right = false;
                                try {
                                   int value1 = Integer.parseInt(FGI.getText());
                                   System.out.println(value1);
                                    for (int i = 0; i<Ans.size() ;i++){
                                Answers ans = (Answers) Ans.get(i);

                                 if (value1 == ans.get_Vaule()){
                                     right = true;
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,value1,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }
                                    }
                                   if (!right){
                                       Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,value1,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                   }
                                    } catch(NumberFormatException nex) {
                                        Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                    }





                    }
                            //MT Q
                            if (ques.get_type() == 6){
                                boolean empty = false;
                                int count = 0;
                                Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();

                                for (int i=0; i<MT.length; i++){
                                  try {
                                      value = Integer.parseInt(MT[i].getText());
                                      } catch(NumberFormatException nex) {
                                          empty = true;
                                      Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,"",ques.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                    }
                                  count++;


                                   for (int j = 0; j<Ans.size() ;j++){
                                    Answers ans = (Answers) Ans.get(j);

                                  if (ans.get_pid()> 0){
                                      if(count==ans.get_order()){


                                         for (int l=0; l<Ans.size();l++){
                                             Answers ans1 = (Answers) Ans.get(l);

                                             if (ans1.get_pid()==0){

                                                 if (ans1.get_order()==value){


                                                     if(ans.get_pid()== ans1.get_id()){
                                                    if(!empty){
                                                         Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),ans1.get_id(),"",ques.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                                         }
                                                     } else {

                                                       if(!empty){
                                                           Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),ans1.get_id(),"",ques.get_id(),true,false,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }

                                                         }
                                                     }
                                                     break;


                                                 }

                                                     }


                                         }
                                      break;
                                      }



                                  }
                                }




                                    }




                    }

                            //SA Q
                            if (ques.get_type() == 7){
                           String answer = SA.getText();
                           boolean right = false;
                           Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                if (answer.isEmpty()){
                                    right = true;
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,ques.get_id(),true,false,0,false,false,true);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }else{
                                  for (int i = 0; i<Ans.size() ;i++){
                                Answers ans = (Answers) Ans.get(i);

                                 if (answer.equalsIgnoreCase(ans.get_text())){
                                    right = true;
                                    Answers_Log ans_log = new Answers_Log(this.C_T_ID,ans.get_id(),0,"",ques.get_id(),true,false,0,true,false,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                 }
                                }
                                  if(!right){
                                     Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,ques.get_id(),true,true,0,false,true,false);

                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }
                                }


                    }
                       }
                            //EA Q
                           if (ques.get_type() == 8){

                            String answer = EA.getText();

                            Answers_Log ans_log = new Answers_Log(this.C_T_ID,0,0,answer,ques.get_id(),false,false,0,false,false,false);
                            Vector<Answers_Log> tempans_L = new Vector<Answers_Log>();
                                     tempans_L.add(ans_log);

                                     if (currentQuestion-1 >= AL_V.size()){

                                     AL_V.add(currentQuestion-1,tempans_L);
                                     }else
                                     {
                                         AL_V.remove(currentQuestion-1);
                                         AL_V.add(currentQuestion-1,tempans_L);
                                     }



                    }


                    //last Question submitetion
            
int n = JOptionPane.showConfirmDialog(
    frame,
    "This is the last Question do you wnat to submit your answers?",
    "Last Question!",
    JOptionPane.YES_NO_OPTION);

if (n == JOptionPane.YES_OPTION) {
                try {
                    Submit();
                } catch (IOException ex) {
                    Logger.getLogger(TestL.class.getName()).log(Level.SEVERE, null, ex);
                }
                
                
} else if (n == JOptionPane.NO_OPTION) {
    
}

			
		}


                                   
                                  
    }
            
void Start_action(ActionEvent e) throws SQLException {
		 cardLayout1.show(jPanel_switch,"main");
		 Next_action(e);
		enable_buttons();
                 //show_current_question();
		 Thread t = new Thread(this);
 		 timer.start();
		 t.start();
    }
    private void enable_buttons() {
        this.Next.setEnabled(true);
        this.Prev.setEnabled(true);
        this.Submit.setEnabled(true);
    }
    void timer_action(ActionEvent e) {
		synchronized (lock) {
			lock.setLock(false);
			seconds--;
			sTime = seconds/60 +":" + ((( seconds%60 ) < 10) ? "0" : "" ) +( seconds%60 ) ;
			
                        lock.notifyAll();
		}
    }

}

