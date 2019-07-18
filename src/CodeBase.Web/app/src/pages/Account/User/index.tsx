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
import { TableListPagination, TableListItem, TableListParams } from '@/data';
import { SorterResult } from 'antd/es/table';
const getValue = (obj: { [x: string]: string[] }) =>
  Object.keys(obj)
    .map(x => obj[x])
    .join(',');

interface IUserListProps extends FormComponentProps {
  dispatch: Dispatch<any>;
  loading: boolean;
  UserList: any; //StateType;
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
      dataIndex: 'fullName',
    },
    {
      title: 'Email',
      dataIndex: 'email',
    },
    {
      title: 'Actions',
    },
  ];

  componentDidMount() {
    console.log(`did mount`);
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
      type: 'user/fetch',
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
    return <div>Users: todo</div>;
  }
}

// export default (): React.ReactNode => (
//   <PageHeaderWrapper title="Users">
//     <div>Todo</div>
//   </PageHeaderWrapper>
// );

export default Form.create<IUserListProps>()(UserList);
