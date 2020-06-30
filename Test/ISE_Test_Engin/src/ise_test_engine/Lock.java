/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package ise_test_engine;

/**
 *
 * @author TOSHIBA
 */
class Lock
{
	private boolean lock=false;

	boolean isLocked()
	{
		return lock;
	}

	void setLock(boolean lock)
	{
		this.lock = lock;
	}
}