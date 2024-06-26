import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { Login } from "./components/Login";
import { Chats} from "./components/Chats";
import { Profielpagina } from "./components/Profielpagina";
import { RegisterErvaringdeskundige} from "./components/RegisterErvaringdeskundige";
import { Register} from "./components/Register";
import { RegisterBedrijf} from "./components/RegisterBedrijf";
import { Onderzoeken} from "./components/Onderzoeken";
import { BedrijvenOnderzoek} from "./components/BedrijvenOnderzoek";
import { BedrijvenPortal} from "./components/BedrijvenPortal";
import { ProfielUpdaten} from "./components/ProfielUpdaten";
import { OnderzoekAanmaken} from "./components/OnderzoekAanmaken";
import { BeheerdersPortal} from "./components/BeheerdersPortal";

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
    path: '/chats',
    element: <Chats />
  },
  {
    path: '/profielpagina',
    element: <Profielpagina />
  },
  {
    path: '/registreer-ervaringdeskundige',
    element: <RegisterErvaringdeskundige />
  },
  {
    path: '/registreer',
    element: <Register/>
  },
  {
    path: '/registreer-bedrijf',
    element: <RegisterBedrijf/>
  },
  {
    path: '/bedrijvenportal',
    element: <BedrijvenPortal/>
  },
  {
    path: '/onderzoeken',
    element: <Onderzoeken/>
  },
  {
    path: '/profielupdaten',
    element: <ProfielUpdaten/>
  },
  {
    path: '/onderzoekaanmaken',
    element: <OnderzoekAanmaken/>
  },
  {
    path: '/bedrijvenonderzoek',
    element: <BedrijvenOnderzoek/>
  },
  {
    path: '/beheerdersportal',
    element: <BeheerdersPortal/>
  }
];

export default AppRoutes;
