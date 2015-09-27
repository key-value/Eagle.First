
<script type="text/html" id="page-template">
    <ul class="pagination">
        <li data-bind="style: {  display:mainModel.pageNum() > 1? 'inline' :'none'},click:function(){mainModel.ReloadBranch(1-mainModel.pageNum())}"><a href="javascript:void(0)"><i class="fa-angle-left"></i></a></li>
        <li data-bind="style: {  display:  mainModel.pageNum() > 3 ?'inline' :'none'},click:function(){mainModel.ReloadBranch(-3)}"><a href="javascript:void(0)">...</a></li>
        <li data-bind="style: {  display:  mainModel.pageNum() > 2 ?'inline' :'none'},click:function(){mainModel.ReloadBranch(-2)} "><a data-bind="html:mainModel.pageNum() - 2" href="javascript:void(0)"></a></li>
        <li data-bind="style: {  display:  mainModel.pageNum() > 1 ?'inline' :'none'} ,click:function(){mainModel.ReloadBranch(-1)}"><a data-bind="html:mainModel.pageNum() - 1" href="javascript:void(0)"></a></li>

        <li class="active"><a href="javascript:void(0)" data-bind="html:mainModel.pageNum()"></a></li>

        <li data-bind="style: {  display:  mainModel.pageNum() + 1 <= mainModel.totalPage() ?'inline' :'none'}"><a data-bind="html:mainModel.pageNum() + 1 ,click:function(){mainModel.ReloadBranch(1)}" href="javascript:void(0)"></a></li>
        <li data-bind="style: {  display:  mainModel.pageNum() + 2 <= mainModel.totalPage() ?'inline' :'none'}"><a data-bind="html:mainModel.pageNum() + 2 ,click:function(){mainModel.ReloadBranch(2)}" href="javascript:void(0)"></a></li>
        <li data-bind="style: {  display:  mainModel.pageNum() + 3 <= mainModel.totalPage() ?'inline' :'none'} "><a href="#">...</a></li>
        <li data-bind="style: {  display:mainModel.pageNum() + 1 <= mainModel.totalPage() ? 'inline' :'none'},click:function(){mainModel.ReloadBranch(mainModel.totalPage() -mainModel.pageNum())}"><a href="#"><i class="fa-angle-right"></i></a></li>
    </ul>
</script>