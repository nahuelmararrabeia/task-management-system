"use client";

import { ReactNode, useEffect } from "react";
import clsx from "clsx";

type DrawerSide = "right" | "bottom";

interface DrawerProps {
  open: boolean;
  onClose: () => void;
  children: ReactNode;
  side?: DrawerSide;
}

export function Drawer({
  open,
  onClose,
  children,
  side = "right",
}: DrawerProps) {
  useEffect(() => {
    function handleKey(e: KeyboardEvent) {
      if (e.key === "Escape") onClose();
    }

    if (open) {
      window.addEventListener("keydown", handleKey);
    }

    return () => window.removeEventListener("keydown", handleKey);
  }, [open, onClose]);

  return (
    <>
      {/* overlay */}
      <div
        onClick={onClose}
        className={clsx(
          "fixed inset-0 bg-black/30 z-40 transition-opacity",
          open ? "opacity-100" : "opacity-0 pointer-events-none"
        )}
      />

      {/* panel */}
      <div
        className={clsx(
          "fixed z-50 bg-white shadow-xl transition-all duration-300 ease-in-out rounded-l-2xl",

          // 👉 RIGHT
          side === "right" && [
            "top-0 right-0 h-full w-100",
            open ? "translate-x-0" : "translate-x-full",
          ],

          // 👉 BOTTOM
          side === "bottom" && [
            "bottom-0 left-0 w-full h-[80%] rounded-t-2xl",
            open ? "translate-y-0" : "translate-y-full",
          ]
        )}
      >
        <div className="h-full overflow-y-auto p-8">{children}</div>
      </div>
    </>
  );
}