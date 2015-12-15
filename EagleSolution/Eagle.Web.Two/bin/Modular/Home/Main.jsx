



var MenuModel = {
    componentWillUnmount() {
        ko.cleanNode(ReactDOM.findDOMNode(this));
    },

    componentDidUpdate() {
        alert(1);
    },

    componentDidMount() {


        this.selectBranch = ko.observable('');

        var thatProps = this.props;
        var thatState = this.states;
        this.__koModel = {
            props:ko.observable(thatProps),
            state: ko.observable(thatState),
            map: ko.observable(''),
            branchAction: function (two) {
                var self = this;
                self.map( '/Modular/'+two.AreaName + '/' + two.ActionName);


                ReactDOM.unmountComponentAtNode(document.getElementById('MainPage'));
                $('#scriptHtml').html('');


                $.ajax({
                    type: "Get",
                    url: self.map() + '.jsx',
                    contentType: "application/text",
                    async: true,
                    success: function(data) {

                        //console.log(data);
                        $('#scriptHtml').html(data);
                        var mainEle = <MainPageModular areaName={two.AreaName} actionName={two.ActionName} pageNum="1"/>; //React.createElement(MainPageModular);
                        //console.log(mainEle);
                        ReactDOM.render(mainEle, document.getElementById('MainPage'));
                    },
                });


            }
        }
        ko.applyBindings(this.__koModel, ReactDOM.findDOMNode(this));
    }
};


var LeftMenu = React.createClass({

    mixins: [ MenuModel ],

    render: function() {
        return (
            <div>
                <div id="scriptHtml" className="hide"></div>
<ul id="main-menu" className="main-menu" data-bind="foreach: props().initialData,as: 'one'">
    <li>
       <a href="#">
           <i data-bind="attr:{ class: Logo}"></i>
           <span className="title" data-bind="text: Title"></span>
       </a>
       <ul data-bind="foreach: {data:Branches ,as: 'two'}">
           <li>

               <a href="#" className="actionBtn" data-bind="click: function(){ $root.branchAction(two)}">
                   <span className="title" data-bind="text: Title"></span>
               </a>
           </li>
       </ul>
    </li>
</ul>
            </div>
      );
}
});

var TitleModel = {
    componentWillUnmount() {
        ko.cleanNode(ReactDOM.findDOMNode(this));
    },

    componentDidUpdate() {

    },
    componentDidMount() {


        var thatProps = this.props;
        var thatState = this.states;
        this.__titlekoModel = {
            props:ko.observable(thatProps),
            state: ko.observable(thatState),
        }

        ko.applyBindings(this.__titlekoModel, ReactDOM.findDOMNode(this));
    }
}


var PageTitle = React.createClass({

    mixins: [ TitleModel ],

    render: function() {
        return (
<div className="page-title">
    <div className="title-env">
        <h1 className="title" data-bind="text: props().title"></h1>
        <p className="description" data-bind="text: props().description"></p>
    </div>
</div>
      );
    }
});


var MainPageModel = {
    componentWillUnmount() {
        ko.cleanNode(ReactDOM.findDOMNode(this));
    },

    componentDidUpdate() {

    },
    componentDidMount() {


        var thatProps = this.props;
        var thatState = this.states;
        this.__mainPagekoModel = {
            props: ko.observable(thatProps),
            state: ko.observable(thatState),
        }

        ko.applyBindings(this.__mainPagekoModel, ReactDOM.findDOMNode(this));
    },


};

var MainPage = React.createClass({

    handleSubmit: function(e){
    this.setState({wocuo:2});
    console.log(111);
},
    mixins: [ TitleModel ],
    render: function() {
        return (
            <div>xxxxx
                <input type="button" value="操作" onClick={this.handleSubmit} />
                <input type="button"  />
            </div>
    );
    }
});
