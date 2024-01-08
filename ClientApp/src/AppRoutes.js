import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Login } from "./components/Login";
import { Profielpagina } from "./components/Profielpagina";

const AppRoutes = [
  {
    index: true,
    element: <Home />
  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
  },
  {
    path: '/login',
    element: <Login />
  },
  {
    path: '/profielpagina',
    element: <Profielpagina />
  }
];

export default AppRoutes;
