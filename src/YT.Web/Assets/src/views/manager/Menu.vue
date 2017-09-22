<template>
       <div class="animated fadeIn">
        <Row>
            <tree-table ref="list"  :columns="cols" :items="cols" >
            </tree-table>
        </Row>
        <!-- 添加和编辑窗口 -->
        <Modal :transfer="false" v-model="modal.isEdit" :title="modal.title" :mask-closable="false" @on-ok="save" @on-cancel="cancel">
            <modify-role  ref="role" :role="modal.current" v-if="modal.isEdit" />
        </Modal>

    </div>
</template>

<script>
import { getUsers, getRoles, getUserForEdit, deleteUser } from 'api/manage';
import modifyMenu from './modify-menu';
export default {
    data() {
        return {
            modal: {
                isEdit: false, title: '添加', current: null
            }
        }
    },
    components: {
        modifyMenu
    },
    created() {
    },
    methods: {
        //删除
        delete(model) {
            var table = this.$refs.list;
            this.$Modal.confirm({
                title: '删除提示', content: "确定要删除当前用户么?",
                onOk: () => {
                    const parms = { id: model.id }
                    deleteUser(parms).then(c => {
                        if (c.data.success) {
                            table.initData();
                        }
                    })
                }
            })
        },
        add() {
            this.modal.isEdit = true;
            this.modal.title = "添加用户";
        },
        edit(row) {
            this.modal.current = row.id;
            this.modal.isEdit = true;
            this.modal.title = "编辑用户:" + row.name;
        },
        save() {
            this.$refs.menu.commit();
        },
        cancel(result) {
            this.modal.isEdit = false;
            this.modal.title = "添加用户";
            this.modal.current = null;
            if (result) {
                this.$refs.list.initData();
            }
        }
    },

    mounted() {
    }
}
</script>


<style type="text/css" scoped>

</style>