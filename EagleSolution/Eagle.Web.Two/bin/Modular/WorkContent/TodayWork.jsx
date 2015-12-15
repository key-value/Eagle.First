
var MainPageModular = React.createClass({

    componentWillMount() {

    },

    componentDidMount() {

        var self = this;
        //function TodayViewModel() {
        //    this.workRecord = ko.observableArray();
        //    this.body = ko.computed(function () {
        //        return {
        //            props: self.props,
        //            state: self.state
        //        };
        //    }, this);

        //};

        //self.todayModel = new TodayViewModel()

        //ko.applyBindings(this.todayModel, ReactDOM.findDOMNode(this));
        $.post(mainModel.PostPath(), {
            AreaName: this.props.areaName,
            ActionName: this.props.actionName
        }, function (data) {
            if (data.Flag) {
                self.setState({ workRecords: data.Fruit });
            };
        });

        //var $dowebok = $("#dowebok");
        //$dowebok.simplerSidebar({
        //    opener: '#abcdf',
        //    sidebar: {
        //        align: 'right',
        //        width: 250
        //    }
        //});

    },
    getInitialState: function () {
        return {
            workRecords: [],
            wocuo: 1,
        };
    },

    handleSubmit: function (event) {
        this.setState({wocuo:2});
        console.log(111);
    },


    render: function () {
        var self = this;
        var tableNodes = this.state.workRecords.map(function (workRecord) {
            return (
            <tr key={workRecord.ID}>
                    <td>{workRecord.Journal ? workRecord.Journal.substr(0, 14) : workRecord.Journal}</td>
                    <td>{workRecord.Summary ? workRecord.Summary.substr(0, 14) : workRecord.Summary}</td>
                    <td>{workRecord.Plan ? workRecord.Plan.substr(0, 14) : workRecord.Plan }</td>
                    <td>{workRecord.CreateTime}</td>
                    <td>
                        <input type="button" className="btn btn-blue" value="操作" onClick={self.handleSubmit} />
                    </td>
            </tr>
            );
        })



        return (
    <div className="panel panel-default" key="1">
        <div id="WorkRecord-part" className="row">
                     <div className="panel-heading">
                         {this.state.wocuo}
                     </div>
                <div className="panel-body">
                    <div className="table-responsive">
                           <table id="WorkRecordTable" className="table">
                               <thead>
                                   <tr>
                                       <th>日志</th>
                                       <th>总结</th>
                                       <th>计划</th>
                                       <th>创建时间</th>
                                       <th>操作</th>
                                   </tr>
                               </thead>
                               <tbody className="middle-align">
                                   {tableNodes}
                               </tbody>
                           </table>
                    </div>
                </div>
        </div>
    </div>
      );
    }
});