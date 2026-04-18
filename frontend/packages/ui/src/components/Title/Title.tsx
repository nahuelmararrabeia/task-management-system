import React from "react";
import clsx from "clsx";

type TitleElement = "h1" | "h2" | "h3" | "h4";
type TitleSize = "xl" | "lg" | "md" | "sm";

interface TitleProps {
  as?: TitleElement;
  size?: TitleSize;
  children: React.ReactNode;
  className?: string;
}

const sizeStyles: Record<TitleSize, string> = {
  xl: "text-3xl font-bold m-4",
  lg: "text-2xl font-semibold m-3",
  md: "text-xl font-semibold",
  sm: "text-lg font-medium",
};

export function Title({
  as: Component = "h1",
  size = "xl",
  children,
  className,
}: TitleProps) {
  return (
    <Component className={clsx(sizeStyles[size], className)}>
      {children}
    </Component>
  );
}