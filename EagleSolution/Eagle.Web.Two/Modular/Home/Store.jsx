
var EventEmitter = {
    _events: {},
    dispatch: function (event, data) {
        if (!this._events[event]) { // 没有监听事件
            return;
        }
        for (var i = 0; i < this._events[event].length; i++) {

            this._events[event][i](data);
        }
    },
    subscribe: function (event, callback) {
        // 创建一个新事件数组
        if (!this._events[event]) {
            this._events[event] = [];
        }
        this._events[event].push(callback);
    }
};


var SecondMenuLi = React.createClass({

    MenuClickHandle :function (event)
    {
        EventEmitter.dispatch( 'RigthReloadAction', this.props.two );
    },
    render:function(){
        return(
            <a href="#" onClick={this.MenuClickHandle}  >
                <span className="title">{this.props.two.Title}</span>
            </a>
        );
    }
});

var SecondMenu = React.createClass({


    render:function(){
       var secondMenuNode = this.props.secondMenuList.map(function(two)
        {
            return (
                <li key={two.ID}>
                    <SecondMenuLi two={two}></SecondMenuLi>
                </li>
            );
        });
        return(
            <ul>
                {secondMenuNode}
            </ul>
        );
    }
});

var LeftMenu = React.createClass({

    componentWillUnmount:function() {
    },

    componentDidUpdate:function() {
    },
    getInitialState: function () {
        return {
            menuList: [],
            initNum: 1,
        };
    },
    componentDidMount() {
        var self = this;
        $.ajax({
            type: "post",
            url: "/Channel/index",
            async: true,
            data:{
                areaName: 'Init',
                actionName: 'LeftMenu'
            },
            success: function(data) {
                if(data.Flag){
                    self.setState({ menuList: data.Fruit });
                }
            }.bind(this),
        });
    },

    render: function() {
        var menuNodes = this.state.menuList.map(function (one,index) {

            return (
                    <li  key={one.SortID} className=" has-sub">
                       <a href="#">
                           <i className={one.Logo}></i>
                           <span className="title" >{one.Title}</span>
                       </a>
                            <SecondMenu secondMenuList={one.Branches}></SecondMenu>
                    </li>
            );
        })
        return (
            <ul id="main-menu" className="main-menu">
                {menuNodes}
            </ul>
      );
    }
});


var RigthHome = React.createClass({
    render: function () {
        return(<h2>Home</h2>)
    }
});


var RightPart = React.createClass({

    componentWillUnmount:function() {
        ReactDOM.unmountComponentAtNode(document.getElementById('RightPart'));
    },
    componentDidUpdate:function() {
    },

    ReloadAction:function(data)
    {
        //var modular = require(['/Modular/'+data.AreaName +'/'+data.ActionName+'.jsx']);
        $.get('/Modular/'+data.AreaName+'/'+data.ActionName+'.jsx',function(){
            var refresh =  eval(data.ActionName + 'Modular');
            refresh.two = data;
            this.setState({Child: refresh});
        }.bind(this));

        this.setState({routerName: data.ActionName});
    },

    componentDidMount() {
        EventEmitter.subscribe( 'RigthReloadAction', this.ReloadAction);
    },
    getInitialState: function () {
        return {
            routerName: 'Home',
            Child : RigthHome,
        };
    },
    render:function()
{
    var Child = this.state.Child;

return(
    <div>
        <Child />
    </div>
);
}
});


ReactDOM.render(React.createElement(LeftMenu), document.getElementById('LeftMenu'));
ReactDOM.render(React.createElement(RightPart), document.getElementById('RightPart'));



