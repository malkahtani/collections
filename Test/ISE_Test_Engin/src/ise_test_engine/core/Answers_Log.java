/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine.core;

/**
 *
 * @author TOSHIBA
 */


public class Answers_Log {
    private int C_T_R_ID;
    private int A_ID;
    private int PID;
    private String Text;
    private int Q_ID;
    private boolean is_marked;
    private boolean need_Rev;
    private int Value;
    private java.sql.Timestamp Date_Submitted;
    private boolean is_right,is_wrong,not_answered;

    public Answers_Log(int c_t_r_ID, int a_id, int pid, String text, int q_id, boolean is_marked, boolean need_rev, int value, boolean Is_right,boolean Is_wrong,boolean Not_answered){
        this.C_T_R_ID = c_t_r_ID;
        this.A_ID = a_id;
        this.PID = pid;
        this.Q_ID = q_id;
        this.Text = text;
        this.is_marked = is_marked;
        this.Value = value;
        this.need_Rev = need_rev;
        this.Date_Submitted = this.get_date1();
        this.is_right = Is_right;
        this.is_wrong = Is_wrong;
        this.not_answered = Not_answered;

    }
    public int get_C_T_R_id(){
        return this.C_T_R_ID;
    }
    public int get_aid(){
        return this.A_ID;
    }
    public int get_pid(){
        return this.PID;
    }
    public int get_qid(){
        return this.Q_ID;

    }
    
    public String get_text(){
        return this.Text;

    }
    public boolean get_right(){
        return this.is_marked;
    }
    public boolean get_need_rev(){
        return this.need_Rev;
    }
    public int get_Vaule(){
        return this.Value;
    }
    public boolean get_is_right(){
        return this.is_right;
    }
    public boolean get_is_wronge(){
        return this.is_wrong;
    }
    public boolean get_not_answered(){
        return this.not_answered;
    }
    public  java.sql.Timestamp  get_date(){
        return this.Date_Submitted;
    }
    private  java.sql.Timestamp  get_date1(){
        
        java.sql.Timestamp date = new java.sql.Timestamp(new java.util.Date().getTime());
        return  date;
    }

    

}
