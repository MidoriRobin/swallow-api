using System;

namespace Swallow.Models.Requests;

    public class UpdateIssueReq
    {
        // For dates, the string formats are expected to be in YYYY/MM/dd (probably more but I havent checked yet)

        private string _name;
        private string _type;
        private string _status;
        private string _description;
        private int? _assignedId;
        private int? _weight;
        private string _dueDate;
        private string _completedDate;
        private int? _timeTaken;
        private int? _sprint;

        // Getters/Setters
        public string? Name 
        { 
            get => _name; 
            set => _name = replaceEmptyWithNull(value); 
        }

        public string? Type 
        { 
            get => _type; 
            set => _type = replaceEmptyWithNull(value);        
        }

        public string? Status 
        { 
            get => _status; 
            set => _status = replaceEmptyWithNull(value);        
        }
        public string? Description 
        { 
            get => _description; 
            set => _description = replaceEmptyWithNull(value);        
        }

        public int? AssignedId
        { 
            get => _assignedId; 
            set => _assignedId = value;
        }

        public int? Weight 
        { 
            get => _weight; 
            set => _weight = value;        
        }

        public string? DueDate 
        { 
            get => _dueDate; 
            set => _dueDate = replaceEmptyWithNull(value);        
        }

        public string? CompletedDate 
        { 
            get => _completedDate;
            set => _completedDate = replaceEmptyWithNull(value);
        }

        public int? TimeTaken 
        { 
            get => _timeTaken; 
            set => _timeTaken = value;
        }

        public int? Sprint 
        { 
            get => _sprint; 
            set => _sprint = value;
        }


        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }

        private int? replaceZeroWithNull(int value)
        {
            // replace empty string with null to make field optional
            return value == 0 ? null : value;
        }

        private DateTime? stringToDate(string dateString) 
        {

            if (string.IsNullOrEmpty(dateString)) return null;

            if (isValidDateString(dateString))
            {
                
            }
            DateTime date = new DateTime(int.Parse(dateString.Split("/")[0]), int.Parse(dateString.Split("/")[1]), int.Parse(dateString.Split("/")[2]));

            return date;
        }

        private bool isValidDateString(string dateString)
        {
            bool isValid = true;

            if(dateString.Contains("/"))
            {
                isValid = dateString.Split("/").Length != 3 ? false : true;

            } else if (dateString.Contains("-"))
            {
                isValid = dateString.Split("-").Length != 3 ? false : true;
            } else 
            {
                isValid = false;
            }

            return isValid;
        }
    }

