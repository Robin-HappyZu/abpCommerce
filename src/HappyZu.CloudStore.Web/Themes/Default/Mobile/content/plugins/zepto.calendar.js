(function ($) {
    $.fn.mdater = function (config) {
        var defaults = {
            maxDate: null,
            minDate: new Date(1970, 0, 1),
            container: $(document.body),
            callback: null,
            value:""
        };
        var option = $.extend(defaults, config);
        //window.console && console.log(this);
        option.container = this;

        //通用函数
        var F = {
            //计算某年某月有多少天
            getDaysInMonth: function (year, month) {
                return new Date(year, month + 1, 0).getDate();
            },
            //计算某月1号是星期几
            getWeekInMonth: function (year, month) {
                return new Date(year, month, 1).getDay();
            },
            getMonth: function (m) {
                return ['一', '二', '三', '四', '五', '六', '七', '八', '九', '十', '十一', '十二'][m];
            },
            //计算年某月的最后一天日期
            getLastDayInMonth: function (year, month) {
                return new Date(year, month, this.getDaysInMonth(year, month));
            }
        }

        //为$扩展一个方法，以配置的方式代理事件
        $.fn.delegates = function (configs) {
            el = $(this[0]);
            for (var name in configs) {
                var value = configs[name];
                if (typeof value == 'function') {
                    var obj = {};
                    obj.click = value;
                    value = obj;
                };
                for (var type in value) {
                    el.delegate(name, type, value[type]);
                }
            }
            return this;
        }

        var mdater = {
            value: {
                year: '',
                month: '',
                date: ''
            },
            lastCheckedDate: '',
            init: function () {
                this.initListeners();
            },
            renderHTML: function () {
                var $html = $('<div class="md_panel"><ul class="md_weekarea"><li>日</li><li>一</li><li>二</li><li>三</li><li>四</li><li>五</li><li>六</li></ul><div class="md_content"></div></div>');
                option.container.html($html);
                return $html;
            },
            _showPanel: function (container) {
                this.refreshView();
            },
            _hidePanel: function () {
                if (option.callback!=null) {
                    option.callback(this.value.year+'-'+(this.value.month+1)+'-'+this.value.date);
                }
            },
            //保存上一次选择的数据
            saveCheckedDate: function () {
                if (this.value.date) {
                    this.lastCheckedDate = {
                        year: this.value.year,
                        month: this.value.month,
                        date: this.value.date
                    }
                }
            },
            //将上一次保存的数据恢复到界面
            setCheckedDate: function () {
                if (this.lastCheckedDate && this.lastCheckedDate.year == this.value.year && this.lastCheckedDate.month == this.value.month) {
                    this.value.date = this.lastCheckedDate.date;
                } else {
                    this.value.date = '';
                }
            },
            //根据日期得到渲染天数的显示的HTML字符串
            getDateStr: function (y, m ,d) {
                var dayStr = '<div class="md_body"><div class="md_head">' + y + '年' + (m+1) + '月</div><ul class="md_datearea in">';
                //计算1号是星期几，并补上上个月的末尾几天
                var week = F.getWeekInMonth(y, m);
                var lastMonthDays = F.getDaysInMonth(y, m - 1);
                for (var j = week - 1; j >= 0; j--) {
                    dayStr += '<li class="prevdate disabled" data-day="' + (lastMonthDays - j) + '"> &nbsp; </li>';
                }
                //再补上本月的所有天;
                var currentMonthDays = F.getDaysInMonth(y, m);
                //判断是否超出允许的日期范围
                var startDay = 1,
                  endDay = currentMonthDays,
                  thisDate = new Date(y, m, d),
                  firstDate = new Date(y, m, 1);
                lastDate = new Date(y, m, currentMonthDays),
                  minDateDay = option.minDate.getDate();


                if (option.minDate > lastDate) {
                    startDay = currentMonthDays + 1;
                } else if (option.minDate >= firstDate && option.minDate <= lastDate) {
                    startDay = minDateDay;
                }

                if (option.maxDate) {
                    var maxDateDay = option.maxDate.getDate();
                    if (option.maxDate < firstDate) {
                        endDay = startDay - 1;
                    } else if (option.maxDate >= firstDate && option.maxDate <= lastDate) {
                        endDay = maxDateDay;
                    }
                }


                //将日期按允许的范围分三段拼接
                for (var i = 1; i < startDay; i++) {
                    dayStr += '<li class="disabled" data-day="' + i + '">' + i + '</li>';
                }
                for (var j = startDay; j <= endDay; j++) {
                    var current = '';
                    if (y == this.value.year && m == this.value.month && this.value.date == j) {
                        current = 'current';
                    }
                    dayStr += '<li class="' + current + '" data-day="' + j + '" data-month="' + m + '" data-year="' + y + '">' + j + '<span>￥100</span></li>';
                }
                for (var k = endDay + 1; k <= currentMonthDays; k++) {
                    dayStr += '<li class="disabled" data-day="' + k + '"> </li>';
                }

                //再补上下个月的开始几天
                var nextMonthStartWeek = (currentMonthDays + week) % 7;
                if (nextMonthStartWeek !== 0) {
                    for (var i = 1; i <= 7 - nextMonthStartWeek; i++) {
                        dayStr += '<li class="nextdate disabled" data-day="' + i + '"> &nbsp; </li>';
                    }
                }
                dayStr += '</ul></div>';
                return dayStr;
            },
            //每次调出panel前，对界面进行重置
            refreshView: function () {
                var initVal = option.value;
                var date = null;
                if (initVal) {
                    var arr = initVal.split('-');
                    date = new Date(arr[0], arr[1] - 1, arr[2]);
                } else {
                    date = new Date();
                }
                 this.value.year = date.getFullYear(),
                 this.value.month = date.getMonth(),
                 this.value.date = date.getDate();
                var y = new Date().getFullYear(),
                    m = new Date().getMonth(),
                    d = new Date().getDate();

                var monthCount = (option.maxDate.getFullYear() - y) * 12 + (option.maxDate.getMonth() - m) + 1;

                var dayStr = '';
                for (var i = 0; i < monthCount; i++) {
                    var ty = y, tm = m ,td=d;
                    if (i > 0) {
                        if (tm + i > 11) {
                            ty += Math.floor((tm+i) / 12);
                        }
                        tm = (tm + i) % 12;
                        td = 0;
                    }
                    dayStr += this.getDateStr(ty, tm ,td);
                }
                $('.md_content').html(dayStr);
            },
            initListeners: function () {
                var _this = this;
                _this.renderHTML();
                var panel = option.container.children('.md_panel');
                _this.afterShowPanel(panel);
                _this._showPanel();
            },
            afterShowPanel: function (panel) {
                var _this = this;
                panel.delegates({
                    '.md_content li': function () {
                        var $this = $(this);
                        if ($this.hasClass('disabled')) {
                            return;
                        }
                        _this.value.year = $this.data('year');
                        _this.value.month = $this.data('month');
                        _this.value.date = $this.data('day');
                        _this.saveCheckedDate();
                        $('.md_content li').removeClass('current');
                        $this.addClass('current');
                        _this._hidePanel();
                    }
                });
            }
        }
        mdater.init();
    }
})(window.Zepto || window.jQuery);