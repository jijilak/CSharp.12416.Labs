namespace CustomAttribute
{
    using System;

    /// <summary>
    ///    This class is a custom attribute that allows
    ///    the name of the developer of a class to be stored
    ///    with the metadata of that class.
    /// </summary>
   
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct, AllowMultiple=true)]
    public class DeveloperInfoAttribute: System.Attribute
    {
		private string developerName;
		private string dateCreated;

		// Constructor. Developer Name is the only mandatory parameter for this attribute
        public DeveloperInfoAttribute(string developerName)
        {
            this.developerName = developerName;
        }

		// Property to return the Developer Name
		public string Developer 
		{
			get
			{
				return developerName;
			}
		}

		// Property to get and set the Creation Date 
		// (an optional parameter for this attribute)
		public string Date
		{
			get
			{
				return dateCreated;
			}

			set
			{
				dateCreated = value;
		 	}
		}
    }
}
