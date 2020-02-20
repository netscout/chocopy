using System;
using System.ComponentModel;

namespace BlackChecker
{
    public class Repository : INotifyPropertyChanged
    {
        private bool _isReserved;
        public bool IsReserved
        {
            get { return _isReserved; }
            set
            {
                _isReserved = value;
                NotifyPropertyChanged("IsReserved");

                if(_isReserved)
                {
                    Reservable = false;
                    Cancelable = true;
                }
                else
                {
                    Reservable = true;
                    Cancelable = false;
                }

                //NotifyPropertyChanged("Reservable");
                //NotifyPropertyChanged("Cancelable");
            }
        }

        private bool _isExpired;
        public bool IsExpired
        {
            get { return _isExpired; }
            set
            {
                _isExpired = value;
                NotifyPropertyChanged("IsExpired");
            }
        }

        private DateTime _now;
        public DateTime Now
        {
            get { return _now; }
            set
            {
                _now = value;
                NotifyPropertyChanged("Now");

                if(_reservedTime > _now)
                {
                    //예약된 시간이랑 비굑해서 남은 시간을 업데이트
                    RemainTime = _reservedTime - _now;
                    
                }
                else
                {
                    RemainTime = TimeSpan.FromSeconds(0);
                }

                //NotifyPropertyChanged("RemainTime");
            }
        }

        private DateTime _reservedTime;
        public DateTime ReservedTime
        {
            get { return _reservedTime; }
            set
            {
                _reservedTime = value;
                NotifyPropertyChanged("ReservedTime");

                if (_reservedTime > _now)
                {
                    //예약된 시간이랑 비굑해서 남은 시간을 업데이트
                    RemainTime = _reservedTime - _now;
                }
                else
                {
                    RemainTime = TimeSpan.FromSeconds(0);
                }

                //NotifyPropertyChanged("RemainTime");
            }
        }

        private TimeSpan _remainTime;
        public TimeSpan RemainTime
        {
            get { return _remainTime; }
            set
            {
                //시간이 마이너스인지 체크 할 필요가 있을듯???
                _remainTime = value;

                NotifyPropertyChanged("RemainTime");

                RemainTimeText = _remainTime.ToString(@"hh\:mm\:ss");
                //NotifyPropertyChanged("RemainTimeText");

                if (IsReserved && _remainTime == TimeSpan.FromSeconds(0))
                {
                    //남아있는 시간이 0인지 확인 필요
                    IsExpired = true;
                    //NotifyPropertyChanged("IsExpired");
                }
            }
        }

        private string _remainTimeText;
        public string RemainTimeText
        {
            get { return _remainTimeText; }
            set
            {
                //시간이 마이너스인지 체크 할 필요가 있을듯???
                _remainTimeText = value;

                NotifyPropertyChanged("RemainTimeText");
            }
        }

        private int _fromDays;
        public int FromDays
        {
            get { return _fromDays; }
            set
            {
                _fromDays = value;

                NotifyPropertyChanged("FromDays");

                //여기서 예약 시간 변경 필요
                ReservedTime = GetReservedDate();
                //NotifyPropertyChanged("ReservedTime");
            }
        }

        private int _fromHours;
        public int FromHours
        {
            get { return _fromHours; }
            set
            {
                _fromHours = value;

                NotifyPropertyChanged("FromHours");

                //여기서 예약 시간 변경 필요
                ReservedTime = GetReservedDate();
                //_reservedTime = GetReservedDate();
                //NotifyPropertyChanged("ReservedTime");
            }
        }

        private int _fromMinutes;
        public int FromMinutes
        {
            get { return _fromMinutes; }
            set
            {
                _fromMinutes = value;

                NotifyPropertyChanged("FromMinutes");

                //여기서 예약 시간 변경 필요
                ReservedTime = GetReservedDate();
                //NotifyPropertyChanged("ReservedTime");
            }
        }

        private bool _reservable;
        public bool Reservable
        {
            get { return _reservable; }
            set
            {
                _reservable = value;

                NotifyPropertyChanged("Reservable");
            }
        }

        private bool _cancelable;
        public bool Cancelable
        {
            get { return _cancelable; }
            set
            {
                _cancelable = value;

                NotifyPropertyChanged("Cancelable");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Repository()
        {
            _isReserved = false;
            _reservable = true;
            _cancelable = false;
        }

        private DateTime GetReservedDate()
        {
            var reservedDate = _now.AddDays(_fromDays);
            int year = reservedDate.Year;
            int month = reservedDate.Month;
            int day = reservedDate.Day;

            _reservedTime = new DateTime(year, month, day, _fromHours, _fromMinutes, 0);
            return _reservedTime;
        }

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
