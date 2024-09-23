package com.agile.repository;

import com.agile.entity.Account;
import org.springframework.data.jpa.repository.EntityGraph;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface AccountRepository extends JpaRepository<Account, Long> {

    @EntityGraph(attributePaths = {"accountRoleList"})
    @Query("select a from Account a where a.accountDbId = :accountDbId")
    Account getWithRolesFromDbId(@Param("accountDbId") Long accountDbId);

    @EntityGraph(attributePaths = {"accountRoleList"})
    @Query("select a from Account a where a.email = :email")
    Account getWithRolesFromEmail(@Param("email") String email);
}
