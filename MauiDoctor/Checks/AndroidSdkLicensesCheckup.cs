﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiDoctor.Doctoring;

namespace MauiDoctor.Checks
{
	public class AndroidSdkLicensesCheckup : Checkup
	{
		public AndroidSdkLicensesCheckup()
		{
		}

		public override string Id => "androidsdklicenses";

		public override string Title => "Android SDK - Accepted Licenses";

		public override async Task<Diagonosis> Examine()
		{
			var android = new Android();

			try
			{
				var v = await android.RequiresLicenseAcceptance();

				if (!v)
					return Diagonosis.Ok(this);
			}
			catch { }

			return new Diagonosis(Status.Error, this, new Prescription("Accept Licenses in Android SDK Manager",
				new ActionRemedy(async (r, ct) =>
				{
					await android.AcceptLicenses();
				})));
		}
	}
}