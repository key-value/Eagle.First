var OperaButton = React.createClass({


    render:function(){
        return(
            <div className="btn-group">
                <button type="button" className="btn btn-blue dropdown-toggle" data-toggle="dropdown">
                    操作 <span className="caret"></span>
                </button>
                <ul className="dropdown-menu dropdown-blue" role="menu">
                    <li>
                        <a href="#">修改</a>
                    </li>
                    <li>
                        <a href="#">删除</a>
                    </li>
                </ul>
            </div>
        );
    },
});
var TodayWorkTr = React.createClass({

    render: function(){
      return(
          <tr>
              <td>{this.props.workRecord.Journal ? this.props.workRecord.Journal.substr(0, 14) : this.props.workRecord.Journal}</td>
              <td>{this.props.workRecord.Summary ? this.props.workRecord.Summary.substr(0, 14) : this.props.workRecord.Summary}</td>
              <td>{this.props.workRecord.Plan ? this.props.workRecord.Plan.substr(0, 14) : this.props.workRecord.Plan }</td>
              <td>{this.props.workRecord.CreateTime}</td>
              <td>
                <OperaButton></OperaButton>
              </td>

          </tr>
      );
    },
});

var TodayWorkModular = React.createClass({
    componentWillMount() {

    },

    componentDidMount() {

        var self = this;
        $.post(mainModel.PostPath(), {
            AreaName: 'WorkContent',
            ActionName: 'TodayWork'
        }, function (data) {
            if (data.Flag) {
                self.setState({ workRecords: data.Fruit });
            };
        });
    },
    getInitialState: function () {
        return {
            workRecords: [],
        };
    },

    OperaAction: function(event){
        console.log($('#system-dialog'));
        $('#system-dialog').modal('show', { backdrop: 'static' });
    },

    render: function () {
        var tableNodes = this.state.workRecords.map(function (workRecord,index) {
            return (
                <TodayWorkTr  workRecord={workRecord} key={index}/>
            );
        })

        return (
            <div className="col-sm-12">
                <div className="panel panel-default">
                    <div id="WorkRecord-part" className="row">
                                 <div className="panel-heading">
                                        <button className="btn btn-blue btn-icon" onClick={this.OperaAction} ><i className="fa-plus"></i>新增日志</button>
                                 </div>
                            <div className="panel-body">
                                <div className="table-responsive">
                                       <table id="WorkRecordTable" className="table table-model-2  table-hover">
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
                <div className="modal fade" id="system-dialog" >
                </div>
            </div>
      );
    }
});


var TodayWork = React.createClass({

    render:function(){
        return(

            <div className="modal-dialog" id="modal-dialog">
                <div className="modal-content">

                    <div className="modal-header">
                        <button type="button" className="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 className="modal-title">日志</h4>
                    </div>

                    <div className="modal-body">

                        <div className="form-horizontal">
                            <div className="row">

                                <div className="form-group">
                                    <label for="Journal" className="col-sm-3 control-label">工作日志</label>
                                    <div className="col-md-9">
                                        <textarea className="form-control" rows="7" placeholder="今天一天的工作内容" id="Journal"></textarea>
                                    </div>

                                </div>
                                <div className="form-group-separator"></div>

                                <div className="form-group">
                                    <label htmlFor="Summary" className="col-sm-3 control-label">工作总结</label>
                                    <div className="col-md-9">
                                        <textarea className="form-control" rows="7" id="Summary" placeholder="一天工作内容总结下吧" ></textarea>
                                    </div>

                                </div>
                                <div className="form-group-separator"></div>

                                <div className="form-group">
                                    <label for="Plan" className="col-sm-3 control-label">明日计划</label>
                                    <div className="col-md-9">
                                        <textarea className="form-control" rows="7" id="Plan" placeholder="明天的有什么新的计划呢"></textarea>
                                    </div>

                                </div>
                                <div className="form-group-separator"></div>


                            </div>
                        </div>
                    </div>


                    <div className="modal-footer">
                        <button type="button" className="btn btn-white" data-dismiss="modal">Close</button>
                        <button type="button" className="btn btn-info">Save changes</button>
                    </div>
                </div>
            </div>

        );
    },
});