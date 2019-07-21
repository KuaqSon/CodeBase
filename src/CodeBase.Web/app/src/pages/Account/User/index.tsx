import {
  Badge,
  Button,
  Card,
  Col,
  DatePicker,
  Divider,
  Dropdown,
  Form,
  Icon,
  Input,
  InputNumber,
  Menu,
  Row,
  Select,
  message,
} from 'antd';
import React, { Component, Fragment } from 'react';
import { PageHeaderWrapper } from '@ant-design/pro-layout';
import { FormComponentProps } from 'antd/lib/form';
import { Dispatch } from 'redux';
import StandardTable, { StandardTableColumnProps } from '@/components/StandardTable';
import { TableListPagination, TableListItem, TableListParams, TableListData } from '@/data';
import { SorterResult } from 'antd/es/table';
import { connect } from 'dva';
import { UserModelState } from '@/models/user';
// import { IUserTableItem, IUserColumnProps } from "./user";
const getValue = (obj: { [x: string]: string[] }) =>
  Object.keys(obj)
    .map(x => obj[x])
    .join(',');

interface IUserListProps extends FormComponentProps {
  dispatch: Dispatch<any>;
  loading: boolean;
  data: TableListData;
}

interface IUserListState {
  expandForm: boolean;
  selectedRows: TableListItem[];
  formValues: { [key: string]: string };
}

class UserList extends Component<IUserListProps, IUserListState> {
  state: IUserListState = {
    expandForm: false,
    selectedRows: [],
    formValues: {},
  };

  columns: StandardTableColumnProps[] = [
    {
      title: 'User name',
      dataIndex: 'userName',
    },
    {
      title: 'Full name',
      dataIndex: 'name',
    },
    {
      title: 'Email',
      dataIndex: 'email',
      render: (text, record: any) => <div>{record.email}</div>,
    },
    {
      title: 'Actions',
    },
  ];

  componentDidMount() {
    console.log(`did mount`);
    const { dispatch } = this.props;

    dispatch({
      type: 'user/all',
      payload: {},
    });
  }

  handleStandardTableChange = (
    pagination: Partial<TableListPagination>,
    filterArg: Record<keyof TableListItem, string[]>,
    sorter: SorterResult<TableListItem>,
  ) => {
    const { dispatch } = this.props;
    const { formValues } = this.state;
    const filters = Object.keys(filterArg).reduce((obj, key) => {
      const clone = { ...obj };
      clone[key] = getValue(filterArg[key]);

      return clone;
    }, {});
    const params: Partial<TableListParams> = {
      currentPage: pagination.current,
      pageSize: pagination.pageSize,
      ...formValues,
      ...filters,
    };

    if (sorter.field) {
      params.sorter = [sorter.field, sorter.order].join(':');
    }

    dispatch({
      type: 'user/all',
      payload: params,
    });
  };

  handleFormReset = () => {
    // todo
  };

  toggleForm = () => {
    // todo
  };

  handleMenuClick = () => {
    // todo
  };

  handleSelectRows = () => {
    // todo
  };

  handleSearch = () => {
    // todo
  };

  handleAdd = () => {
    // todo
  };

  renderSearchSimpleForm() {
    return null;
  }

  renderSearchAdvanceForm() {
    return null;
  }

  renderSearchForm() {
    return null;
  }

  render() {
    const { data, loading } = this.props;

    const { selectedRows } = this.state;

    // const menu = (
    //   <Menu>
    //     <Menu.Item key="remove">Remove</Menu.Item>
    //     <Menu.Item key="approval">Approval</Menu.Item>
    //   </Menu>
    // );

    return (
      <PageHeaderWrapper title="Users">
        <Card>
          <StandardTable
            selectedRows={selectedRows}
            loading={loading}
            data={data}
            columns={this.columns}
            onSelectRow={this.handleSelectRows}
            onChange={this.handleStandardTableChange}
          />
        </Card>
      </PageHeaderWrapper>
    );
  }
}

const mapStateToProps = ({
  user: { data },
  loading = false,
}: {
  user: UserModelState;
  loading: boolean;
}) => ({
  data,
  loading,
});

export default Form.create<IUserListProps>()(connect(mapStateToProps)(UserList));
