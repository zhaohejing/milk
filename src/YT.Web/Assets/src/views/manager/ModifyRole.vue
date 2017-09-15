<template>
    <Tabs value="role">
        <TabPane label="角色信息" name="role">
            <Row>
                <Col :md="22">
                <Form :model="current.role" :rules="ruleValidate" :label-width="100">
                    <FormItem label="角色名称" prop="displayName">
                        <Input v-model="current.role.displayName" placeholder="请输入角色名称"></Input>
                    </FormItem>
                    <FormItem label="是否启用">
                        <i-switch v-model="current.role.isActive" size="large">
                            <span slot="open">启用</span>
                            <span slot="close">禁用</span>
                        </i-switch>
                    </FormItem>
                    <FormItem label="是否默认角色">
                        <i-switch v-model="current.role.isDefault" size="large">
                            <span slot="open">是</span>
                            <span slot="close">否</span>
                        </i-switch>
                    </FormItem>
                    <FormItem label="角色介绍">
                        <Input v-model="current.role.description" type="textarea" :autosize="{minRows: 5,maxRows: 5}" placeholder="请输入..."></Input>
                    </FormItem>
                </Form>
                </Col>

            </Row>

        </TabPane>
        <TabPane label="权限信息" name="permissions">
            <Row>
                <Col :offset="2" :md="22">
                <Tree ref="tree" :data="permissionTrees" multiple show-checkbox></Tree>

                </Col>

            </Row>
        </TabPane>
    </Tabs>
</template>
<script>
import { saveRole, getRoleForEdit } from 'api/manage';
import { allPermissions } from 'api/menu';

export default {
    props: {
        role: {
            type: Number,
            default() {
                return null
            }
        }
    },
    data() {
        return {
            current: {
                role: {
                    id: this.role,
                    displayName: null,
                    isDefault: false,
                    isActive: false,
                    description: null
                },
                grantedPermissionNames: []
            },
            permissionTrees: [],
            ruleValidate: {
                displayName: [
                    { required: true, message: '角色名不能为空', trigger: 'blur' }
                ]
            }
        }
    },
    created() {
        this.init();
    },
    mounted() {

    },
    methods: {
        async init() {
            getRoleForEdit({ id: this.current.role.id }).then(c => {
                if (c.data.success) {
                    this.current = c.data.result;
                    this.initTree();
                }
            })
        },
        async initTree() {
            allPermissions().then(c => {
                if (c.data.success) {
                    /*转换树结构*/
                    this.permissionTrees = this.$converToTreedata(c.data.result.items,
                        null,
                        'parentId',
                        this.current.grantedPermissionNames)
                }
            })
        },
        commit() {
            debugger;
            var nodes = this.$refs.tree.getCheckedNodes();
            if (nodes) {
                let temp = [];
                nodes.forEach(c => {
                    temp.push(c.name);
                })
                this.current.grantedPermissionNames = temp;
            } else {
                this.current.grantedPermissionNames = null;
            }
            saveRole(this.current).then(r => {
                if (r.data.success) {
                    this.$emit('submit-complete');
                }
            });
        }

    }
}
</script>
