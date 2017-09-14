<template>
    <div class="animated fadeIn">
        <Row>
            <Col :md="20">
            <Form ref="params" :model="params" inline :label-width="60">
                <FormItem label="用户名">
                    <Input v-model="params.name" placeholder="请输入用户名"></Input>
                </FormItem>
                <FormItem label="手机">
                    <Input v-model="params.mobile" placeholder="请输入手机">
                    </Input>
                </FormItem>
                <FormItem label="角色">
                    <Select v-model="params.role" placeholder="请选择">
                        <Option value="beijing">北京市</Option>
                        <Option value="shanghai">上海市</Option>
                        <Option value="shenzhen">深圳市</Option>
                    </Select>
                </FormItem>
                <FormItem>
                    <Button type="primary" shape="circle" icon="ios-search" @click="searchApi">查询</Button>
                </FormItem>
            </Form>
            </Col>
            <Col :md="2" :offset="2">
            <Button type="primary">添加</Button>
            </Col>
        </Row>
        <Row>
            <milk-table :columns="cols" :search-api="searchApi" :params="params" />
        </Row>
    </div>
</template>

<script>
import { getRoles,getUsers } from 'api/manage';
export default {
    name: 'account',
    data() {
        return {
            cols: [
                {
                    type: 'selection',
                    align: 'center'
                },
                {
                    title: '用户名',
                    key: 'name'
                },
                {
                    title: '角色',
                    key: 'age'
                },
                {
                    title: '注册时间',
                    key: 'address'
                },
                {
                    title: '操作',
                    key: 'action',
                    align: 'center',
                    render: (h, params) => {
                        return h('div', [
                            h('Button', {
                                props: {
                                    type: 'primary',
                                    size: 'small'
                                },
                                style: {
                                    marginRight: '5px'
                                },
                                on: {
                                    click: () => {
                                        this.edit(params.row)
                                    }
                                }
                            }, '编辑'),
                            h('Button', {
                                props: {
                                    type: 'error',
                                    size: 'small'
                                },
                                on: {
                                    click: () => {
                                        this.remove(params.row)
                                    }
                                }
                            }, '删除')
                        ]);
                    }
                }
            ],
            searchApi:getUsers,
            params: { name: '', role: null, mobile: null },
            roles: []
        }
    },
    created() {
        this.getRoles();
    },
    methods: {
        getRoles() {
            const params={
                maxResultCount: 99,
                skipCount: 0
            };
            getRoles(params).then(c => {
                this.roles = c.result;
            })
        }

    },
    mounted() {
    }
}
</script>


<style type="text/css" scoped>

</style>