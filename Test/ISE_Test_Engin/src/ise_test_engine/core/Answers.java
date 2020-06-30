/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine.core;

/**
 *
 * @author TOSHIBA
 */
public class Answers implements Comparable{
    private int DB_ID;
    private int PID;
    private String Text;
    private int Q_ID;
    private boolean is_Right;
    private int A_Order;
    private int Value;

    public Answers(int db_id, int pid, String text, int q_id, boolean is_right, int order, int value){
        this.DB_ID = db_id;
        this.PID = pid;
        this.Q_ID = q_id;
        this.Text = text;
        this.is_Right = is_right;
        this.A_Order = order;
        this.Value = value;



    }
    public int get_id(){
        return this.DB_ID;
    }
    public int get_pid(){
        return this.PID;
    }
    public int get_qid(){
        return this.Q_ID;

    }
    public int get_order(){
            return this.A_Order;

    }
    public String get_text(){
        return this.Text;

    }
    public boolean get_right(){
        return this.is_Right;
    }
    public int get_Vaule(){
        return this.Value;
    }
    public int compareTo(Answers A) {
    final int BEFORE = -1;
    final int EQUAL = 0;
    final int AFTER = 1;

    //this optimization is usually worthwhile, and can
    //always be added
    if ( this == A ) return EQUAL;

    //primitive numbers follow this form
    if  (this.A_Order < A.A_Order)  return BEFORE;
         
    if (this.A_Order > A.A_Order){ return AFTER;
   

    }else{
        return EQUAL;
    }
}

    public int compareTo(Object o) {
        return compareTo((Answers) o);
    }
}
