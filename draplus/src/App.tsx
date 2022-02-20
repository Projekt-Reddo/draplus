import "./App.css";
import { BrowserRouter as Router } from "react-router-dom";
import { QueryClient, QueryClientProvider } from "react-query";

import BaseRoutes from "routes";

function App() {
    return (
        <QueryClientProvider client={new QueryClient()}>
            <Router>
                <BaseRoutes />
            </Router>
        </QueryClientProvider>
    );
}

export default App;
