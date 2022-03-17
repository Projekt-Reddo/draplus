import * as React from "react";
import Icon from "./Icon";

interface ButtonBanUserProps {
    user: any;
}

const ButtonBanUser: React.FC<ButtonBanUserProps> = ({ user }) => {
    if (user.isBanned) {
        return (
            <div className="iconBan">
                <Icon icon="ban" className="mt-[9px]" />
            </div>
        );
    }

    return (
        <div className="iconLock">
            <Icon icon="lock-open" className="mt-[9px]" />
        </div>
    );
};

export default ButtonBanUser;
