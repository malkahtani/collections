/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine.core;

/**
 *
 * @author M.alkahtani
 */
public interface iA {
	public String        getXML();
	public String        getText();
	public void          setText(String text);
	public boolean       isCorrect();
	public void          setCorrect(boolean correct);
	public void          setCorrect(String correct);

	public boolean       isSelected();
}
