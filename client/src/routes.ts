// @material-ui/icons
import Dashboard from '@material-ui/icons/Dashboard';
import Person from '@material-ui/icons/Person';
import LibraryBooks from '@material-ui/icons/LibraryBooks';
// import BubbleChart from '@material-ui/icons/BubbleChart';
// import LocationOn from '@material-ui/icons/LocationOn';
// import Notifications from '@material-ui/icons/Notifications';
// core components/views for Admin layout
import DashboardPage from './views/Dashboard/Dashboard';
import UserProfile from './views/UserProfile/UserProfile';
import TableList from './views/TableList/TableList';
import Typography from './views/Typography/Typography';
import Categories from './views/Categories/Categories';
import Transactions from './views/Transactions/Transactions';
// import Icons from './views/Icons/Icons';
// import Maps from './views/Maps/Maps';
// import NotificationsPage from './views/Notifications/Notifications';

const dashboardRoutes = [
  {
    path: '/dashboard',
    name: 'Dashboard',
    rtlName: 'لوحة القيادة',
    icon: Dashboard,
    component: DashboardPage,
    layout: '/admin'
  },
  {
    path: '/user',
    name: 'User Profile',
    rtlName: 'ملف تعريفي للمستخدم',
    icon: Person,
    component: UserProfile,
    layout: '/admin'
  },
  {
    path: '/table',
    name: 'Table List',
    rtlName: 'قائمة الجدول',
    icon: 'content_paste',
    component: TableList,
    layout: '/admin'
  },
  {
    path: '/typography',
    name: 'Typography',
    rtlName: 'طباعة',
    icon: LibraryBooks,
    component: Typography,
    layout: '/admin'
  },
  {
    path: '/category',
    name: 'Category',
    rtlName: 'طباعة',
    icon: LibraryBooks,
    component: Categories,
    layout: '/admin'
  },
  {
    path: '/transaction',
    name: 'Tranaction',
    rtlName: 'طباعة',
    icon: 'content_paste',
    component: Transactions,
    layout: '/admin'
  },
  // {
  //   path: '/icons',
  //   name: 'Icons',
  //   rtlName: 'الرموز',
  //   icon: BubbleChart,
  //   component: Icons,
  //   layout: '/admin'
  // },
  // {
  //   path: '/maps',
  //   name: 'Maps',
  //   rtlName: 'خرائط',
  //   icon: LocationOn,
  //   component: Maps,
  //   layout: '/admin'
  // },
  // {
  //   path: '/notifications',
  //   name: 'Notifications',
  //   rtlName: 'إخطارات',
  //   icon: Notifications,
  //   component: NotificationsPage,
  //   layout: '/admin'
  // }
];

export default dashboardRoutes;
