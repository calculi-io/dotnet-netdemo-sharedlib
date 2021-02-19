using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;

// nuget add .\SharedLib.1.0.1.nupkg -source "c:\windows\temp"

namespace SharedLib {
	public class RegistryOperations {

		private const string messageLocation = @"Software\Calculi";
		private const string messageKey = "message";
		public readonly string HelloMessageDefault = "Hello from the server.";

		public string GetMessage() {
			string message = HelloMessageDefault;
			RegistryKey key = null;

			try {
				key = Registry.CurrentUser.OpenSubKey(messageLocation);
				if (null != key) {
					Object val = key.GetValue(messageKey);
					if (null != val) {
						message = val.ToString();
					}
				}				
			} catch(Exception ex) {
				message = ex.Message;
			} finally {
				if (null != key) {
					key.Close();
				}
			}

			return message;
		}

		public bool SetMessage(string message) {
			bool success = false;
			RegistryKey key = null;

			try {
				key = Registry.CurrentUser.CreateSubKey(messageLocation, true);
				if (null == key) {
					return false;
				}
				
				key.SetValue(messageKey, message);
				success = true;
			} catch {
				success = false;
			} finally {
				if (null != key) {
					key.Close();
				}
			}

			return success;
		}

		public bool DeleteMessage() {
			bool success = false;
			RegistryKey key = null;

			try {
				key = Registry.CurrentUser.OpenSubKey(messageLocation, true);
				if (key == null) {
					return true;
				}
				foreach (var k in key.GetValueNames()) {
					if (k == messageKey) {
						key.DeleteValue(messageKey);
					}
				}				
				success = true;
			} catch {
				success = false;
			} finally {
				if (null != key) {
					key.Close();
				}
			}

			return success;
		}
	}
}
